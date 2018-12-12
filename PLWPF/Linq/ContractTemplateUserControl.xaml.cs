using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ContractTemplateUserControl.xaml
    /// </summary>
    public partial class ContractTemplateUserControl : UserControl
    {
        BL.IBL bl;
        public ContractTemplateUserControl()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void moreDetails_Click(object sender, RoutedEventArgs e)
        {
            object OBJ = this.DataContext;
            MessageBox.Show(
                OBJ.ToString(),
                "Contract Details");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is BE.Contract)
            {
                BE.Contract c = (BE.Contract)this.DataContext;
                BE.Nanny n = bl.GetNanny(c.NannyId);
                BE.Child ch = bl.GetChild(c.ChildId);
                BE.Mother m = bl.GetMother(ch.MotherId);

                this.NannyLabel.Content = $"Nanny name: {n.FirstName} {n.LastName}";
                this.ChildLabel.Content = $"Child name: {ch.FirstName} {m.LastName}";
                this.MotherLabel.Content = $"Mother name: {m.FirstName} {m.LastName}";
                this.DateLabel.Content = $"Date: {c.StartContractDate.ToShortDateString()} - {c.EndContractDate.ToShortDateString()}";
                this.SumLabel.Content = $"Total: {c.NetoRate}₪ ";
            }

        }
    }
}
