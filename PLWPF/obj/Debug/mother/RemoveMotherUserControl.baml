   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      #    PLWPF.RemoveMotherUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5         motherBackground#��[  5	        � 	Resources  5
      (   "PLWPF.BooleanToVisibilityConverter   BooleanToVisibilityConverter(  "PLWPF.BooleanToNotBooleanConverter   BooleanToNotBooleanConverter%  PLWPF.NullToVisibilityConverter  NullToVisibilityConverter��%W   &	       &	      &	 &       5   
   6-     5   
   6-     5   
   6+   5      .��5      �� .��5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��Remove Mother��6#     motherTitle#��[ 6<   $��Center=�6a   $��0��6~   $
��0,15q�6�   �� 5      $��1��6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ MotherDataGrid��5         
IsReadOnly .    AutoGenerateColumns .  $
��auto��6�   $��Center=�6�    �� SelectedItem 6a   �� *mother+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        Id��,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��5   U   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   ��  �� Path$ Id�6a     5      -   5        	FirstName��$	 
First Name��5   c   $

 auto 6w    6@   �� $ 	FirstName�6w     5      -   5        LastName��$	 	Last Name��5   a   $

 auto 6t    6?   �� $ LastName�6t   4	  .System.Windows.Controls.DataGridTemplateColumn	  5       	  CellTemplate 5      �� 5    "   .R�5!   &   ��  ��Name$ moreDataGridButton��6-   -   5        $��More details��5!   J   6a   5"   #   5$      	  5%       5&      �� 5'   "   .R�5(   &   �� $ deleteDataGridButton��6-   -   5        $��Delete��5(   L   6]   5)   #   5+      5-      �� 5.      -   5        $ noResult��5.      $��No mother was found��6*   $��5q�6H   $��Center=�6S   y�6p   �� *ItemsSource+ �� 	Converter# [  �� ElementName$ MotherDataGrid��5/      51      52      53      