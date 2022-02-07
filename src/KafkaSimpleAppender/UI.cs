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

        private async void bSend_Click(object sender, EventArgs e)
        {
            try
            {
                var topic = new Topic(tbTopic.Text);
                var message = new Models.Message(tbKey.Text, tbMessage.Text);

                await _sender.SendAsync(topic, message, CancellationToken.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
