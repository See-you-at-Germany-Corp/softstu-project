namespace soft_stu_project.Models{
    public class LabItem{
        public LabItem(string id, string title, int quantity, int leftQuantity){
            this.id = id;
            this.title = title;
            this.quantity = quantity;
            this.newQuantity = 0;
            this.leftQuantity = leftQuantity;
        }
        string id {get;}
        public string title {get; set;}
        public int quantity {get; set;}
        public int newQuantity {get; set;}
        public int leftQuantity {get; set;}
    }
}