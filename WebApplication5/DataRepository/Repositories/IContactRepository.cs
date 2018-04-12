using DataTrasferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Repositories
{
    public interface IContactRepository
    {
        List<ContactDTO> GetContactList();
        ResponseDTO AddContactDetails(ContactDTO contactDTO);
        ResponseDTO UpdateContactDetails(ContactDTO contactDTO);
        ResponseDTO DeleteContactDetails(ContactDTO contactDTO);
    }
}
