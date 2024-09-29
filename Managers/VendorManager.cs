using Models;

namespace Managers
{
    public class VendorManager :MainManager<Vendor>
    {  
        public VendorManager(ProjectContext context) :base(context) {
        }
    }
}
