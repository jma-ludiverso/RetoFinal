using System;
using System.Globalization;
using Xamarin.Forms;

namespace Mario.Behaviors
{
    public class DateEntryBehavior : Behavior<Entry>
    {

        private string _lastValidText;

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += EntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= EntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void EntryTextChanged(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            if (entry != null)
            {
                bool isValid = false;
                DateTime value;
                if (string.IsNullOrEmpty(entry.Text) ||
                    DateTime.TryParseExact(entry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out value))
                {
                    isValid = true;
                }

                ((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
            }
        }

    }
}
