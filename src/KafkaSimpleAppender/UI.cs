using Logic;

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
            await _sender.SendAsync(new Models.Message("", ""), CancellationToken.None);
        }
    }
}
