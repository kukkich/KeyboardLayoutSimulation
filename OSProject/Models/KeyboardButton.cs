namespace OSProject.Models
{
    public class KeyboardButton
    {
        public int Id { get; set; }
        public char? Value { get; set; }

        public KeyboardButton(int id, char? value)
            => (Id, Value) = (id, value);
    }
}
