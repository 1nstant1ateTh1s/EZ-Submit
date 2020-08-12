using EZSubmitApp.Core.Entities.Base;

namespace EZSubmitApp.Core.Entities
{
    public class DocxAttachment : IntBaseEntity
    {
        public DocxAttachment()
        {
        }

        #region Properties
        public string OutputName { get; set; }
        public byte[] Content { get; set; }
        public CaseForm CaseForm { get; set; }
        public int CaseFormId { get; set; }
        #endregion
    }
}
