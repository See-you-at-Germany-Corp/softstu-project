namespace softstu_project.Models{
    public class LabItem{
        public LabItem(string id, string title, string am, string pm){
            this.id = id;
            this.title = title;
            this.am = am;
            this.newQuantity = 0;
            this.pm = pm;
        }
        public string id {get;}
        public string title {get; set;}
        public string am {get; set;}
        public int newQuantity {get; set;}
        public string pm {get; set;}
    }
}