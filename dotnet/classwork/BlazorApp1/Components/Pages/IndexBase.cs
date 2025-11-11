using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Pages
{
    public class IndexBase : ComponentBase
    {
        public string Text { get; set; } = "click me";
        protected void ChangeText()
        {
            if (Text == "click me")
            {
                Text = "you clicked me";
            }
            else
            {
                Text = "click me";
            }
        }
        public string ChangeColor { get; set; } = "btn btn-primary";    
    }
}
