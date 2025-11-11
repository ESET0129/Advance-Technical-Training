using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Pages
{
    public class CounterBase : ComponentBase

    {
        public int currentCount = 0;

        public void IncrementCount()
        {
            currentCount++;
        }
    }
}
