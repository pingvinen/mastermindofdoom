namespace Pingvinen.MasterMindOfDoom
{
    public class Guess
    {
        public virtual Code Code { get; set; } = new Code();
        public virtual Feedback Feedback { get; set; } = new Feedback();

        public virtual bool IsCorrect => Feedback.ValueAndPositionMatches == Code.Length;
    }
}