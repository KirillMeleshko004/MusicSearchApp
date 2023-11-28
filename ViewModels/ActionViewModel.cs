using MusicSearchApp.Models;

namespace MusicSearchApp.ViewModels
{
    public class ActionViewModel
    {
        public int ActionId { get; set; }
        public DateTime Date { get; set; }
        public ProfileViewModel Actor { get; set; } = null!;
        public string Description { get; set; } = null!;


        public ActionViewModel(Models.Action action)
        {
            ActionId = action.ActionId;
            Date = action.Date;
            Description = action.Description;
            if(action.Actor != null)
                Actor = new(action.Actor);
        }
    }
}