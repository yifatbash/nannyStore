   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      %    PLWPF.RemoveContractUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5         contractBackground#��[  5	        � 	Resources  5      (   "PLWPF.BooleanToVisibilityConverter   BooleanToVisibilityConverter(  "PLWPF.BooleanToNotBooleanConverter   BooleanToNotBooleanConverter%  PLWPF.NullToVisibilityConverter  NullToVisibilityConverter��%W   &	       &	      &	 &       5   
   6-     5   
   6-     5   
   6+   5      .��5      �� $��Center=�6   ��.  �� PanningMode$ HorizontalOnly=�6O    J� SnapsToDevicePixels . $��Visible=�6�   .��5   
   � $��Center=�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� -   5         ��Name$ title��5      $��Remove Contract��6#     contractTitle#��[ 6>   $��2��6e   $��0��6y   $��0,15,0,0q�6�   �� 5      -   5         ��Name$  contractCategoryStackPanel��5      $��0��6=   $��1��6M   $��Stretch=�6Z   $��	10,0,0,10q�6x   .+�5       �� -   5        $ contractCategoryLabel��5       $��Get contract By:��67     subTitle#��[ 6R   $��0,0,0,5q�6t   �� 5!      $
��20,0q�6   .+�5"      	� -   5         	�Name$ nannyRadioButton��5"   "   $��Nanny��6<   	� 5#      -   5        $ inValidRadioButton��5#   "   $��Invalide contracts��6>   6[   	� 5$      -   5        $ allRadioButton��5$   "   $	��All��6:   6H   5%      5&      �� 5'      -   5         ��Name$ empty��5'      $	��120��6'   $��1��63   $��1��6@   �� 5)      -   5        $ nannyStackPanel��5)      $��1��62   $��1��6B   $��10q�6O   y�6\   �� 	 �� ElementName$	 nannyRadioButton��
 �� Path$
 	IsChecked� �� 	Converter# [ .+�5*      �� $��Select nanny:��6   $��Center=�60   �� 5+      -	   5         ��Name$ nannyComboBox��5+      $��Center=�62   $	��120��6O   6[   5,      �� 5.      -
   5        $ contractDataStackPanel��5.      $��2��69   $��2��6F   $��10q�6Z   $��Center=�6f   .+�5/      &   System.Windows.Controls.DataGrid  -   5          Name$ contractDataGrid��5/         
IsReadOnly .    AutoGenerateColumns .   �� SelectedItem 6d   �� *
contract+ �� Mode$ TwoWay=�   Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      -   5        
ContractId��,  &System.Windows.Controls.DataGridColumn   Header$ Contract Id��51   e      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� $
 
ContractId�6z     52      -   5        NannyId��$ Nanny Id��52   _   $
 auto 6q    6>   �� $
 NannyId�6q     53      -   5        ChildId��$ Child Id��53   _   $
 auto 6q    6>   �� $
 ChildId�6q     54      -   5        	start��$ 
Start Date��54   �   $
 auto 6�    6<   �� $
 StartContractDate� �� StringFormat$ dd/MM/yy��6�     55      -   5        end��$ End Date��55   |   $
 auto 6�    6:   �� $
 EndContractDate�$ dd/MM/yy��6�   4	  .System.Windows.Controls.DataGridCheckBoxColumn	  56      -   5        IsSigned��$ 	Is Signed��56   m   $
 auto 6�    6C   �� $
 IsSignedContract�6�   4
  .System.Windows.Controls.DataGridTemplateColumn
  57       
  CellTemplate 58      �� 59   "   .R�5:   &   ��  ��Name$ moreDataGridButton��6-   -   5        $��More details��5:   J   6a   5;   #   5=      
  5>       5?      �� 5@   "   .R�5A   &   �� $ deleteDataGridButton��6-   -   5        $��Delete��5A   L   6]   5B   #   5D      5F      �� 5G      -   5        $ noResult��5G      $��No contract was found��6*   $��5q�6J   $��Center=�6U   y�6r   �� *ItemsSource+# [ $	 contractDataGrid��5H      5I      5K      5L      