   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      #    PLWPF.UpdateMotherUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      -   5           motherBackground#��[  5
        � 	Resources  5      (  "PLWPF.BooleanToNotBooleanConverter   BooleanToNotBooleanConverter��%   &	         5   
   6-   5      .��5      �� $��Center=�6   ��.  �� PanningMode$ HorizontalOnly=�6O    J� SnapsToDevicePixels . $��Visible=�6�    � DataContext 6�   �� .��5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��Update Mother��6#     motherTitle#��[ 6<   $��2��6a   $��0��6u   $��0,5,0,15q�6�   �� 5      -   5         ��Name$ SelectChildIdStackPanel��5      $��1��6:   .+�5      �� $��Select mother��6     subTitle#��[ 60   $��Center=�6R   $��3q�6o   $��Center=�6z   �� 5      -   5         ��Name$ motherComboBox��5      $��Center=�63   $��23��6P   $��3q�6\   $��Center=�6�   $��60��6�    �� SelectedItem 6g   �� *Id+	 �� Mode$	 OneTime=�
 �� NotifyOnValidationError
 .  �� ValidatesOnExceptions . 6�   5      � 5      -   5         �Name$ motherDetailsGrid��5      $��center=�6/   $��10q�6L   $��2��6X   $	��Top=�6e   {�.   � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'    5!      �� 5"      $
��Auto �6$   �� 5#      $
��Auto �6$   �� 5$      $
��auto �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��auto �6$   �� 5)      $
��Auto �6$   �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$   ��5/      �� 50       �� Triggers 51      �� 52       �� Binding 6*   ��  �� ElementName$ motherComboBox�� �� Path$ SelectedIndex� �� Value -16m   .a�53   "   ��  �� Property � {� �� Value .  6C   54      56      .+�58      �� $��* Id:��6   $��0��6(   $
��Left=�68   $��3q�6S   $��0��6^   $��Center=�6k   �� 59      -   5         ��Name$ idTextBlock��59      $��1��61   $
��Left=�6A   $��23��6\   $��3q�6h   $��0��6s   $��Center=�6�   $	��120��6  ��6�   �� *Id+$	 TwoWay=� �� ValidatesOnDataErrors . 
 .  . 6  �� 5:      $��* First Name:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��1��6f   $��Center=�6s   �� 5;      -   5         ��Name$ firstNameTextBox��5;      $��1��64   $
��Left=�6D   $��23��6_   $��3q�6k   $��1��6v   $��Center=�6  $	��120��6  ��6�   �� *	FirstName+$	 TwoWay=� . 
 .  . 6  �� 5<      $��* Last Name:��6   $��0��6/   $
��Left=�6?   $��3q�6Z   $��2��6e   $��Center=�6r   �� 5=      -   5        $ lastNameTextBox��5=      $��1��63   $
��Left=�6C   $��23��6^   $��3q�6j   $��2��6u   $��Center=�6�   $	��120��6  ��6�   �� *
LastName+$	 TwoWay=� . 
 .  . 6  �� 5>      $��* Phone Number:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��3��6h   $��Center=�6u   �� 5?      -	   5        $ phoneNumberTextBox��5?      $��1��66   $
��Left=�6F   $��23��6a   $��3q�6m   $��3��6x   $��Center=�6  $	��120��6   ��6�   �� *PhoneNumber+$	 TwoWay=� . 
 .  . 6   �� 5@      $��* Home Address:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��4��6h   $��Center=�6u      PLWPF.PlaceAutoCompleteUC  5A      -
   5          Name$ homeAddressTextBox��5A   ,   $��1��6H   $
��Left=�6X   $��23��6s   $��3q�6   $��4��6�   $��Center=�6  $	��120��62     Text 6�   �� *HomeAddress+$	 TwoWay=� . 
 .  . 62  �� 5B      $��Wanted Nanny Address:��6   $��0��68   $
��Left=�6H   $��3q�6c   $��5��6n   $��Center=�6{     5C      -   5        $ wantedNannyAddressTextBox��5C   ,   $��1��6O   $
��Left=�6_   $��23��6z   $��3q�6�   $��5��6�   $��Center=�6%  $	��120��6@   6�   �� *WantedNannyAddress+$	 TwoWay=� . 
 .  . 6@  �� 5D      $��Is Single Mother:��6   $��0��64   $
��Left=�6D   $��3q�6_   $��7��6j   $��Center=�6w   �� 5E      -   5         ��Name$ isSingleMotherCheckBox��5E      $�� ��6;   $��1��6F   $
��Left=�6V   $��3q�6�   $��7��6�   $��Center=�6�    d� 	IsChecked 6q   �� *IsSingleMother+$	 TwoWay=�
 .  . 6�   �� 5F      $��Status:��6   $��0��6*   $
��Left=�6:   $��3q�6U   $��6��6`   $��Center=�6m   �� 5G      -   5        $ statusComboBox��5G      $��1��63   $
��Left=�6C   $��23��6^   $��3q�6j   $��6��6u   $��Center=�6�   $	��120��6�    �� Text 6�   �� *Status+$	 TwoWay=�
 .  . 6�   9� 5I      -   5         9�Name$ motherSchedule��5I      $��Mother Scedule��63   $��8��6K   $��2��6X   $��5q�6l   .��5J      &   System.Windows.Controls.DataGrid  -   5          Name$ motherScheduleDataGrid��5J      $��0��6l      AutoGenerateColumns .  $��2��6�       EnableRowVirtualization  . $	��0,3q�6�   $��3��6�   $��7��6�   $��Stretch=�6�   !   RowDetailsVisibilityMode$! VisibleWhenSelected=�6  ��6?   �� *WantedNannySchedule+"   Columns" 5K      0  *System.Windows.Controls.DataGridTextColumn  5L      -   5        	dayColumn��,  &System.Windows.Controls.DataGridColumn#   Header$	# Day��5L   a   $   Width5  /System.Windows.Controls.DataGridLengthConverter$
$ auto 6n   %   
IsReadOnly% . 1  +System.Windows.Controls.DataGridBoundColumn&   Binding& 6D   �� $	 Day�6{   4  .System.Windows.Controls.DataGridCheckBoxColumn  5M      -   5        isWorkDayColumn��$# Is Work Day��5M   ~   $$ SizeToHeader 6�   & 6N   �� $ 	IsWorkDay�$	 TwoWay=�6�     5N      -   5        startHourColumn��$# 
Start Hour��5N   �   $$ SizeToHeader 6�   & 6K   �� $ 	StartHour�$	 TwoWay=�' �� StringFormat$' 
{0:hh\:mm}��% 6�   �� *	IsWorkDay+$	 OneWay=�( �� 	Converter#( [   5O      -   5        endHourColumn��$# End Hour��5O   H   $$ SizeToHeader 6�   & 6Z   �� $ EndHour�$	 TwoWay=�$' 
{0:hh\:mm}��% 6�   �� *	IsWorkDay+$	 OneWay=�#( [ 5Q      5R      9� 5T      -   5        $ commentsExpander��5T      $��Comments��65   $��2��6G   $��9��6[   $��Stretch=�6h   $��5q�6�   .��5U      %	   PLWPF.UpdateCommentsUserControl	  ) 	  Text) 66   �� *
Comments+$	 TwoWay=�5V      �� 5X      $��* = Required fields��6   $��2��66   $��10��6J   $
��Bold"�6X   $��Center=�6j   �� 5Z      -   5        * ��Name$* UpdateMotherButton��5Z      $��Update��65   $��11��6F   $��0��6T   $��2��6d   $��Stretch=�6x   $��16(�6�   $��15q�6�   ��5[      �� 5\       5]      �� 5^       6*   �� $ firstNameTextBox��$
 Text�  6f   .a�5_   "   ��  � {� .  6E   5`      �� 5a       6*   �� $ lastNameTextBox��$
 Text�  6e   .a�5b   "   ��  � {� .  6E   5c      �� 5d       6*   �� $ homeAddressTextBox��$
 Text� 6h   Z� .a�5e   "   ��  � {� .  6E   5f      �� 5g       6*   �� $ homeAddressTextBox��$
 Text�  6h   .a�5h   "   ��  � {� .  6E   5i      �� 5j       6*   �� $ statusComboBox��$ SelectedIndex� -16m   .a�5k   "   ��  � {� .  6E   5l      �� 5m       6*   �� $ phoneNumberTextBox��$
 Text�  6h   .a�5n   "   ��  � {� .  6E   5o      5q      5s      5t      5u      5v      5w      