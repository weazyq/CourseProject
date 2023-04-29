using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp.Utils
{
    public class DigitTextBox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            // Проверяем, содержит ли введенный символ цифру, точку или дефис
            if (!char.IsDigit(e.Text, e.Text.Length - 1)
                && e.Text != "."
                && e.Text != "-")
            {
                e.Handled = true; // Отменяем ввод неподходящего символа
            }

            base.OnPreviewTextInput(e);
        }
    }
}
