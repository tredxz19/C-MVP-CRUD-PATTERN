using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSHARPCRUDMVC.Views
{
    public interface IMainView
    {
        event EventHandler ShowPetView;
        event EventHandler ShowOwnerView;
        event EventHandler ShowVetsView;
    }

}
