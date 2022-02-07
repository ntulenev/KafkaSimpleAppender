using Logic;
using Models;

namespace KafkaSimpleAppender
{
    public partial class UI : Form
    {
        public UI(IKafkaSender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));

            InitializeComponent();

            cbTypes.DataSource = Enum.GetValues(typeof(KeyType));
        }

        private readonly IKafkaSender _sender;

#pragma warning disable IDE1006 // Naming Styles
        private async void bSend_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            try
            {
                DisableUI();

                await SendDataAsync();

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

        private async Task SendDataAsync()
        {
            var topic = new Topic(tbTopic.Text);

            switch ((KeyType)cbTypes.SelectedItem)
            {
                case KeyType.StringKey:
                    {
                        var message = new Models.Message<string>(tbKey.Text, tbMessage.Text);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                case KeyType.LongKey:
                    {
                        var message = new Models.Message<long>(long.Parse(tbKey.Text), tbMessage.Text);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                case KeyType.NoKey:
                    {
                        var message = new Models.Message<object>(null!, tbMessage.Text);
                        await _sender.SendAsync(topic, message, CancellationToken.None);
                        break;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(cbTypes.SelectedItem));
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        private void cbTypes_SelectedIndexChanged(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            CheckMessageTypeUI();
        }

        private void CheckMessageTypeUI()
        {
            if ((KeyType)cbTypes.SelectedItem == KeyType.NoKey)
            {
                tbKey.Clear();
                tbKey.Enabled = false;
            }
            else
            {
                tbKey.Enabled = true;
            }
        }
    }
}
