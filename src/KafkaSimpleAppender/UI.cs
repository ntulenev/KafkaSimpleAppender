using Logic;
using Models;
using System.Text;

namespace KafkaSimpleAppender
{
    public partial class UI : Form
    {
        public UI(IKafkaSendHandler hander, IFileLoader fileLoader)
        {
            _hander = hander ?? throw new ArgumentNullException(nameof(hander));
            _fileLoader = fileLoader ?? throw new ArgumentNullException(nameof(fileLoader));

            InitializeComponent();

            cbTypes.DataSource = _hander.ValidKeyTypes;
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            CheckUIType();
        }
        private void rbSingle_CheckedChanged(object sender, EventArgs e)
        {
            CheckUIType();
        }

        private async void bLoadFile_Click(object sender, EventArgs e)
        {
            var result = opDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    DisableUI();

                    _fileMessages = await _fileLoader.LoadAsync(opDialog.FileName, CancellationToken.None);
                    _fileName = opDialog.SafeFileName;

                    MessageBox.Show("Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowFileData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    EnableUI();
                }
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        private async void bSend_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            try
            {
                DisableUI();

                if (rbFile.Checked)
                {
                    IProgress<int> progress = new Progress<int>(v =>
                    {
                        ShowFileData(v);
                    });

                    await _hander.HandleAsync(tbTopic.Text,
                          (KeyType)cbTypes.SelectedItem,
                          _fileMessages,
                          cbJson.Checked,
                          x => progress.Report(x),
                          CancellationToken.None);
                }
                else
                {
                    await _hander.HandleAsync(tbTopic.Text,
                          (KeyType)cbTypes.SelectedItem,
                          tbKey.Text,
                          tbMessage.Text,
                          cbJson.Checked,
                          CancellationToken.None);
                }

                Clear();

                MessageBox.Show("Success", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableUI();
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            CheckMessageTypeUI();
        }

        private void DisableUI()
        {
            Enabled = false;
        }

        private void EnableUI()
        {
            Enabled = true;
        }

        private void Clear()
        {
            tbKey.Clear();
            tbMessage.Clear();
        }

        private void CheckMessageTypeUI()
        {
            if (rbFile.Checked)
            {
                return;
            }

            if ((KeyType)cbTypes.SelectedItem == KeyType.NotSet)
            {
                tbKey.Clear();
                tbKey.Enabled = false;
            }
            else
            {
                tbKey.Enabled = true;
            }
        }

        private void CheckUIType()
        {
            if (rbFile.Checked)
            {
                tbKey.Clear();
                tbMessage.Clear();
                tbKey.Enabled = false;
                tbMessage.Enabled = false;
                bLoadFile.Enabled = true;
            }
            else
            {
                rbFileLog.Clear();
                bLoadFile.Enabled = false;
                tbMessage.Enabled = true;
                CheckMessageTypeUI();
            }
        }

        private void ShowFileData(int? progress = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"File : {_fileName}");
            sb.AppendLine($"Items count : {_fileMessages.Count}");
            if (progress != null)
            {
                sb.AppendLine($"Loading progress: {progress}/{_fileMessages.Count}");
            }
            rbFileLog.Text = sb.ToString();
        }

        private readonly IKafkaSendHandler _hander;
        private readonly IFileLoader _fileLoader;

        private string _fileName = null!;
        private IReadOnlyCollection<KeyValuePair<string, string>> _fileMessages = null!;
    }
}
