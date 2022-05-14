using Logic;
using Models;

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
                    await _hander.HandleAsync(tbTopic.Text,
                          (KeyType)cbTypes.SelectedItem,
                          _fileMessages,
                          cbJson.Checked,
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

        private readonly IKafkaSendHandler _hander;
        private readonly IFileLoader _fileLoader;

        private IEnumerable<KeyValuePair<string, string>> _fileMessages = null!;
    }
}
