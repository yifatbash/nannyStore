   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5           PLWPF.AddMotherUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      -   5           motherBackground#��[  5
        � 	Resources  5      (  "PLWPF.BooleanToNotBooleanConverter   BooleanToNotBooleanConverter��%   &	         5   
   6-   5      .��5      �� $��Center=�6   ��.  �� PanningMode$ HorizontalOnly=�6O    J� SnapsToDevicePixels . $��Visible=�6�    � DataContext 6�   �� .��5   
   � $��Center=�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��
Add Mother��6#     motherTitle#��[ 69   $��2��6^   $��0��6r   $��0,5,0,15q�6   � 5      -   5         �Name$ motherDetailsGrid��5      $��center=�6/   $��10q�6L   $��1��6X   $	��Top=�6e    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'    5!      �� 5"      $
��Auto �6$   �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��Auto �6$   �� 5)      $
��Auto �6$   �� 5*      $
��Auto �6$   .+�5,      �� $��* Id:��6   $��0��6(   $
��Left=�68   $��3q�6S   $��0��6^   $��Center=�6k   �� 5-      -   5         ��Name$ 	idTextBox��5-      $��1��6-   $
��Left=�6=   $��23��6X   $��3q�6d   $��0��6o   $��Center=�6�   $	��120��6  ��6|   �� *Id+	 �� Mode$	 TwoWay=�
 �� ValidatesOnDataErrors
 .  �� NotifyOnValidationError .  �� ValidatesOnExceptions . 6  �� 5.      $��* First Name:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��1��6f   $��Center=�6s   �� 5/      -   5        $ firstNameTextBox��5/      $��1��64   $
��Left=�6D   $��23��6_   $��3q�6k   $��1��6v   $��Center=�6  $	��120��6  ��6�   �� *	FirstName+$	 TwoWay=�
 .  .  . 6  �� 50      $��* Last Name:��6   $��0��6/   $
��Left=�6?   $��3q�6Z   $��2��6e   $��Center=�6r   �� 51      -   5        $ lastNameTextBox��51      $��1��63   $
��Left=�6C   $��23��6^   $��3q�6j   $��2��6u   $��Center=�6�   $	��120��6  ��6�   �� *
LastName+$	 TwoWay=�
 .  .  . 6  �� 52      $��* Phone Number:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��3��6h   $��Center=�6u   �� 53      -   5        $ phoneNumberTextBox��53      $��1��66   $
��Left=�6F   $��23��6a   $��3q�6m   $��3��6x   $��Center=�6  $	��120��6   ��6�   �� *PhoneNumber+$	 TwoWay=�
 .  .  . 6   �� 54      $��* Home Address:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��4��6h   $��Center=�6u      PLWPF.PlaceAutoCompleteUC  55      -   5          Name$ homeAddressTextBox��55   ,   $��1��6H   $
��Left=�6X   $��23��6s   $��3q�6   $��4��6�   $��Center=�6  $	��120��62     Text 6�   �� *HomeAddress+$	 TwoWay=�
 .  .  . 62  �� 56      $��Wanted Nanny Address:��6   $��0��68   $
��Left=�6H   $��3q�6c   $��5��6n   $��Center=�6{     57      -	   5        $ wantedNannyAddressTextBox��57   ,   $��1��6O   $
��Left=�6_   $��23��6z   $��3q�6�   $��5��6�   $��Center=�6%  $	��120��6@   6�   �� *WantedNannyAddress+$	 TwoWay=�
 .  .  . 6@  �� 58      $��Is Single Mother:��6   $��0��64   $
��Left=�6D   $��3q�6_   $��7��6j   $��Center=�6w   �� 59      -
   5         ��Name$ isSingleMotherCheckBox��59      $�� ��6;   $��1��6F   $
��Left=�6V   $��3q�6�   $��7��6�   $��Center=�6�    d� 	IsChecked 6q   �� *IsSingleMother+$	 TwoWay=� .  . 6�   �� 5:      $��Status:��6   $��0��6*   $
��Left=�6:   $��3q�6U   $��6��6`   $��Center=�6m   �� 5;      -   5         ��Name$ statusComboBox��5;      $��1��63   $
��Left=�6C   $��22��6^   $��3,0,0,4q�6j   $��6��6{   $��Bottom=�6�   $	��120��6   �� Text 6�   �� *Status+$	 TwoWay=� .  . 6  5<      9� 5>      -   5         9�Name$ motherSchedule��5>      $��Mother Scedule��6/   $
��left=�6G   $��2��6b   $��5q�6o   .��5?      &   System.Windows.Controls.DataGrid  -   5          Name$ motherScheduleDataGrid��5?      $��0��6h      AutoGenerateColumns .  $��2��6�      EnableRowVirtualization . $	��0,3q�6�   $��3��6�   $��7��6�   $��Center=�6�      RowDetailsVisibilityMode$ VisibleWhenSelected=�6  ��6;   �� *WantedNannySchedule+   Columns 5@      0  *System.Windows.Controls.DataGridTextColumn  5A      -   5        	dayColumn��,  &System.Windows.Controls.DataGridColumn   Header$	 Day��5A   ]      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6j      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   ��  �� Path$	 Day�6w   4  .System.Windows.Controls.DataGridCheckBoxColumn  5B      -   5        isWorkDayColumn��$ Is Work Day��5B   z   $ SizeToHeader 6�    6J   �� $ 	IsWorkDay�$	 TwoWay=�6�     5C      -   5        startHourColumn��$ 
Start Hour��5C   �   $ SizeToHeader 6�    6G   �� $ 	StartHour�$	 TwoWay=� �� StringFormat$ 
{0:hh\:mm}�� 6�   �� *	IsWorkDay+$	 OneWay=� �� 	Converter# [   5D      -   5        endHourColumn��$ End Hour��5D   D   $ SizeToHeader 6�    6V   �� $ EndHour�$	 TwoWay=�$ 
{0:hh\:mm}�� 6�   �� *	IsWorkDay+$	 OneWay=�# [ 5F      5G      9� 5H      -   5        $ CommentsGrid��5H      $��Comments��6-   $��2��6?   $��3��6S   $
��left=�6`   $��5,2,5,2q�6{   .��5I      	   PLWPF.CommentsUserControl	  -   5          	 Name$  CommentsExpander��5I   ,   ! 	  Text! 6F   �� *
Comments+$	 TwoWay=� .  . 
 . 5J      �� 5K      $��* = Required fields��6   $��4��62   $
��Bold"�6?   $��Center=�6Q   �� 5L      -   5        " ��Name$" AddMotherButton��5L      $	��Add��6.   $��5��6<   $��0��6I   $��2��6Y   $��Stretch=�6m   $��16(�6�   $��10q�6�   ��5M      �� 5N      # �� Triggers# 5O      �� 5P      $ �� Binding$ 6*   �� % �� ElementName$% firstNameTextBox��$
 Text�& �� Value&  6f   .a�5Q   "   �� ' �� Property' � {�( �� Value( .  6E   5R      �� 5S      $ 6*   �� $% 	idTextBox��$
 Text�& 06_   .a�5T   "   �� ' � {�( .  6E   5U      �� 5V      $ 6*   �� $% lastNameTextBox��$
 Text�&  6e   .a�5W   "   �� ' � {�( .  6E   5X      �� 5Y      $ 6*   �� $% homeAddressTextBox��$
 Text�& 6h   Z� .a�5Z   "   �� ' � {�( .  6E   5[      �� 5\      $ 6*   �� $% homeAddressTextBox��$
 Text�&  6h   .a�5]   "   �� ' � {�( .  6E   5^      �� 5_      $ 6*   �� $% statusComboBox��$ SelectedIndex�& -16m   .a�5`   "   �� ' � {�( .  6E   5a      �� 5b      $ 6*   �� $% phoneNumberTextBox��$
 Text�&  6h   .a�5c   "   �� ' � {�( .  6E   5d      5f      5h      5i      5j      5k      