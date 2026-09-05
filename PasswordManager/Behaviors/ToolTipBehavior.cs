using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace PasswordManager.Client.Behaviors
{
    /// <summary>
    /// Измененое поведение ToolTip для кнопки с паролем, чтобы отображать сообщение "Скопировано!" после нажатия.
    /// </summary>
    public static class ToolTipBehavior
    {
        private const int DefaultDelayMs = 1500;
        private const string DefaultOriginalText = "Скопировать";
        private const string DefaultSuccessText = "Скопировано!";

        public static readonly AttachedProperty<bool> AttachProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>(
                "Attach",
                typeof(ToolTipBehavior),
                false);

        static ToolTipBehavior()
        {
            AttachProperty.Changed.Subscribe(OnAttachChanged);
        }

        public static void SetAttach(Control element, bool value) => element.SetValue(AttachProperty, value);
        public static bool GetAttach(Control element) => element.GetValue(AttachProperty);

        private static void OnAttachChanged(AvaloniaPropertyChangedEventArgs<bool> e)
        {
            if (e.Sender is Interactive interactive)
            {
                if (e.NewValue.Value)
                {
                    interactive.AddHandler(Button.ClickEvent, OnButtonClick, RoutingStrategies.Bubble, handledEventsToo: true);
                }
                else
                {
                    interactive.RemoveHandler(Button.ClickEvent, OnButtonClick);
                }
            }
        }

        private static async void OnButtonClick(object? sender, RoutedEventArgs e)
        {
            if (sender is Control control)
            {
                ToolTip.SetTip(control, DefaultSuccessText);
                ToolTip.SetIsOpen(control, true);

                await Task.Delay(DefaultDelayMs);

                ToolTip.SetTip(control, DefaultOriginalText);
            }
        }
    }
}
