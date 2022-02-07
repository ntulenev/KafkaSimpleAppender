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
        }

        private readonly IKafkaSender _sender;

#pragma warning disable IDE1006 // Naming Styles
        private async void bSend_Click(object sender, EventArgs e)
#pragma warning restore IDE1006 // Naming Styles
        {
            try
            {
                DisableUI();

                var topic = new Topic(tbTopic.Text);
                var message = new Models.Message<string>(tbKey.Text, tbMessage.Text);

                await _sender.SendAsync(topic, message, CancellationToken.None);

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
    }
}
