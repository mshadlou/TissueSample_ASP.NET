namespace TissueSample2.Client.Services
{
    public interface IModal
    {
        bool ModalWarningView { get; set; }
        string ModalMessage { get; set; }
        Object OBJ { get; set; }
        void ViewModal(string message, bool status);
        void ViewModal(string message, bool status, Object obj);
        void Cancle();
        void Approve();
    }
    public class ModalManager: IModal
    {
        public bool ModalWarningView { get; set; }
        public string ModalMessage { get; set; }
        public Object OBJ { get; set; }

        public void ViewModal(string message, bool status)
        {
            ModalWarningView = status;
            ModalMessage = message;
        }

        public void ViewModal(string message, bool status, Object obj)
        {
            OBJ = obj;
            ModalWarningView = status;
            ModalMessage = message;
        }

        public void Cancle()
        {
            ViewModal("", false);
        }

        public virtual void Approve()
        {
            Console.WriteLine("Modal has been confirmed");
        }
    }
}
