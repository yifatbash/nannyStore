   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      "    PLWPF.RemoveNannyUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5         nannyBackground#��[  5	        � 	Resources  5      (   "PLWPF.BooleanToVisibilityConverter   BooleanToVisibilityConverter(  "PLWPF.BooleanToNotBooleanConverter   BooleanToNotBooleanConverter%  PLWPF.NullToVisibilityConverter  NullToVisibilityConverter��%W   &	       &	      &	 &       5   
   6-     5   
   6-     5   
   6+   5      .��5      �� .��5   
   � $��Center=�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��Remove Nanny��6#     
nannyTitle#��[ 6;   $��0��6_   $
��0,15q�6l   �� 5      $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ NannyDataGrid��5         
IsReadOnly .    AutoGenerateColumns .  $��Center=�6�    �� SelectedItem 6a   �� *nanny+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        Id��,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��5   U   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ��  �� Path$ Id�6a     5      -   5        	FirstName��$	 
First Name��5   c   $

 auto 6w    6@   �� $ 	FirstName�6w     5      -   5        LastName��$	 	Last Name��5   a   $

 auto 6t    6?   �� $ LastName�6t     5      -   5        	BirthDate��$	 	BirthDate��5   ~   $

 auto 6�    6@   �� $ 	BirthDate� �� StringFormat$ 
dd/MM/yyyy��6�     5       -   5        Address��$	 Address��5    _   $

 auto 6p    6>   �� $ Address�6p   4	  .System.Windows.Controls.DataGridTemplateColumn	  5"       	  CellTemplate 5#      �� 5$   "   .R�5%   &   ��  ��Name$ moreDataGridButton��6-   -   5        $��More details��5%   J   6a   5&   #   5(      	  5)       5*      �� 5+   "   .R�5,   &   �� $ deleteDataGridButton��6-   -	   5        $��Delete��5,   L   6]   5-   #   5/      51      �� 52      -
   5        $ noResult��52      $��No nanny was found��6*   $��5q�6G   $��Center=�6R   y�6o   �� *ItemsSource+ �� 	Converter# [  �� ElementName$ NannyDataGrid��53      56      57      59      