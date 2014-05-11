using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ARK.View;
using ARK.ViewModel.Base.Interfaces;
using ARK.ViewModel.Protokolsystem;
using KeyboardEventArgs = ARK.ViewModel.Protokolsystem.KeyboardEventArgs;

namespace ARK.ViewModel.Base
{
    public class KeyboardContainerViewModelBase : PageContainerViewModelBase, IKeyboardContainerViewModelBase
    {
        private bool _enableKeyboard;
        private OnScreenKeyboard _keyboard;
        private FrameworkElement _currentSelectedTextBox;

        public KeyboardContainerViewModelBase()
        {
            // Setup keyboard listener
            KeyboardTextChanged += (sender, e) =>
            {
                if (CurrentSelectedTextBox == null) return;

                var textBox = CurrentSelectedTextBox as TextBox;
                if (textBox != null)
                    textBox.Text = KeyboardText;
            };
        }

        public OnScreenKeyboard Keyboard
        {
            get
            {
                if (_keyboard != null) return _keyboard;

                Keyboard = new OnScreenKeyboard();
                KeyboardHide();

                return Keyboard;
            }
            set
            {
                _keyboard = value;
                Notify();
            }
        }

        public ICommand KeyboardToggle
        {
            get
            {
                return GetCommand<object>(e =>
                {
                    // If keyboardstate is on ignore check for currentSelectedTextBox
                    if (KeyboardToggled)
                    {
                        KeyboardToggled = false;
                        return;
                    }

                    // Check if a textbox has been selected
                    // If true change keyboard state
                    if (CurrentSelectedTextBox != null)
                        KeyboardToggled = !KeyboardToggled;
                });
            }
        }

        private FrameworkElement CurrentSelectedTextBox
        {
            get { return _currentSelectedTextBox; }
            set
            {
                // Unbind event if current is not null
                if (_currentSelectedTextBox != null)
                {
                    var ctextbox = CurrentSelectedTextBox as TextBox;
                    if (ctextbox != null)
                        ctextbox.TextChanged -= textbox_TextChanged;
                }

                _currentSelectedTextBox = value;

                // Update keyboard text and bind event
                var textbox = CurrentSelectedTextBox as TextBox;
                if (textbox != null)
                {
                    // Set text
                    KeyboardText = textbox.Text;

                    // Bind eventhandler to support twoway
                    textbox.TextChanged += textbox_TextChanged;
                }
            }
        }

        void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update keyboard text
            var textbox = CurrentSelectedTextBox as TextBox;
            if (textbox != null)
            {
                // Set text
                KeyboardText = textbox.Text;
            }
        }

        public ICommand GotFocus
        {
            get { return GetCommand<FrameworkElement>(element => { CurrentSelectedTextBox = element; }); }
        }

        public bool KeyboardToggled
        {
            get { return _enableKeyboard; }
            set
            {
                _enableKeyboard = value;
                Notify();
            }
        }

        public string KeyboardText
        {
            get { return ((KeyboardViewModel) Keyboard.DataContext).Text; }
            protected set { ((KeyboardViewModel) Keyboard.DataContext).Text = value; }
        }

        public void KeyboardShow()
        {
            KeyboardToggled = true;
        }

        public void KeyboardHide()
        {
            KeyboardToggled = false;
        }

        public event EventHandler<KeyboardEventArgs> KeyboardTextChanged
        {
            add { ((KeyboardViewModel) Keyboard.DataContext).TextChanged += value; }
            remove { ((KeyboardViewModel) Keyboard.DataContext).TextChanged -= value; }
        }

        public void KeyboardClear()
        {
            KeyboardText = "";
        }

        public override void NavigateToPage(Func<FrameworkElement> page, string pageTitle)
        {
            base.NavigateToPage(page, pageTitle);

            // Remove currentselected textbox
            CurrentSelectedTextBox = null;

            // Hide and clear keyboard
            KeyboardText = "";
            KeyboardHide();
        }
    }
}