   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5          PLWPF.AddChildUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      -   5           childBackground#��[  5
        � 	Resources  5      (   "PLWPF.BooleanToVisibilityConverter   BooleanToVisibilityConverter��%   &	         5   
   6-   5      .��5      �� $��Center=�6   ��.  �� PanningMode$ HorizontalOnly=�6O    J� SnapsToDevicePixels . $��Visible=�6�    � DataContext 6�   �� .��5   
   � $��Center=�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��Add New Child��6#     
childTitle#��[ 6<   $��2��6`   $��0��6t   $��0,15,0,0q�6�   � 5      -   5         �Name$ childDetailsGrid��5      $��1��6-   $��10,30q�6:    5       �� 5!      $
��auto �6$   �� 5"      $
��auto �6$   �� 5#      $
��auto �6$   �� 5$      $
��auto �6$    � ColumnDefinitions 5'      �� 5(      $
��auto �6'   �� 5)      $
��auto �6'   .+�5+      �� $��* Id:��6   $��0��6(   $
��Left=�68   $��3q�6S   $��0��6^   $��Center=�6k   �� 5,      -   5         ��Name$ 	idTextBox��5,      $��1��6-   $
��Left=�6=   $��23��6X   $��3q�6d   $��0��6o   $��Center=�6�   $	��120��6  ��6|   �� *Id+	 �� Mode$	 TwoWay=�
 �� NotifyOnValidationError
 .  �� ValidatesOnExceptions .  �� ValidatesOnDataErrors . 6  �� 5-      $��	* Mother:��6   $��0��6,   $
��Left=�6<   $��3q�6W   $��1��6b   $��Center=�6o   �� 5.      -   5         ��Name$ motherIdComboBox��5.      $��1��65   $
��Left=�6E   $��23��6`   $��3q�6l   $��1��6w   $��Center=�6
  $	��120��6%   �� SelectedValue 6�   �� *
MotherId+$	 TwoWay=�
 .  .  . 6%  �� 5/      $��* First Name:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��2��6f   $��Center=�6s   �� 50      -   5        $ firstNameTextBox��50      $��1��64   $
��Left=�6D   $��23��6_   $��3q�6k   $��2��6v   $��Center=�6  $	��120��6  ��6�   �� *	FirstName+$	 TwoWay=�
 .  .  . 6  �� 51      $��* Birth Date:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��3��6f   $��Center=�6s   (  "System.Windows.Controls.DatePicker  52      -   5          Name$ birthDateDatePicker��52      $��1��6:   $
��Left=�6J   $��3q�6e   $��3��6p   $��Center=�6     SelectedDate 6}   �� *	BirthDate+$	 TwoWay=�
 .  .  . 6  53      �� 56      -   5         ��Name$# isSpecialNeedsChildStackPanel��56      $��2��6@   $
��10,0q�6M   .+�57      � -	   5        $ isSpecialNeedsChildGrid��57      $
��Left=�68   $	��Top=�6S    58      �� 59      $
��Auto �6+   �� 5:      $
��Auto �6+    5<      �� 5=      $
��Auto �6(   �� 5>      $
��Auto �6(   .+�5@      �� $��Is Special Needs Child:��6   $��0��6>   $
��Left=�6N   $��3q�6i   $��0��6t   $��Center=�6�   �� 5A      -
   5         ��Name$! isSpecialNeedsChildCheckBox��5A      $�� ��6D   $��1��6O   $
��Left=�6_   $��3q�6�   $��0��6�   $��Center=�6   d� 	IsChecked 6z   �� *IsSpecialNeedsChild+$	 TwoWay=�
 .  . 6     PLWPF.CommentsUserControl  5B      -   5          Name$ SpecialNeedsText��5B   0   $��1��6u   $��2��6�      Text 6J   �� *SpecialNeeds+$	 TwoWay=�y�6�   ��  �� ElementName$! isSpecialNeedsChildCheckBox�� �� Path$ 	IsChecked� �� 	Converter# [ 5C      5D      �� 5F      -   5        $ isFoodAllergyStackPanel��5F      $��3��6:   $
��10,0q�6G   .+�5G      � -   5        $ isFoodAllergyGrid��5G      $
��Left=�62   $	��Top=�6M    5H      �� 5I      $
��Auto �6+   �� 5J      $
��Auto �6+    5L      �� 5M      $
��Auto �6(   �� 5N      $
��Auto �6(   .+�5P      �� $��Is Food Allergy:��6   $��0��67   $
��Left=�6G   $��3q�6b   $��0��6m   $��Center=�6z   �� 5Q      -   5        $ isFoodAllergyCheckBox��5Q      $�� ��6>   $��1��6I   $
��Left=�6Y   $��3q�6�   $��0��6�   $��Center=�6�    6t   �� *IsFoodAllergy+$	 TwoWay=�
 .  . 6    5R      -   5        $ FoodAllergyText��5R   0   $��1��6s   $��2��6�    6I   �� *FoodAllergy+$	 TwoWay=�y�6�   �� $ isFoodAllergyCheckBox��$ 	IsChecked�# [ 5S      5T      �� 5U      -   5        $" isMedicinesAllergyStackPanel��5U      $��4��6?   $
��10,0q�6L   .+�5V      � -   5        $ isMedicinesAllergyGrid��5V      $
��Left=�67   $	��Top=�6R    5W      �� 5X      $
��Auto �6+   �� 5Y      $
��Auto �6+    5[      �� 5\      $
��Auto �6(   �� 5]      $
��Auto �6(   .+�5_      �� $��Is Medicines Allergy:��6   $��0��6<   $
��Left=�6L   $��3q�6g   $��0��6r   $��Center=�6   �� 5`      -   5        $  isMedicinesAllergyCheckBox��5`      $�� ��6C   $��1��6N   $
��Left=�6^   $��3,8,0,0q�6�   $��0��6�   $	��Top=�6   6y   �� *IsMedicinesAllergy+$	 TwoWay=�
 .  . 6    5a      -   5        $ MedicinesAllergyText��5a   0   $��1��6}   $��2��6�    6N   �� *MedicinesAllergy+$	 TwoWay=�y�6�   �� $  isMedicinesAllergyCheckBox��$ 	IsChecked�# [ 5b      5c      � 5e      -   5        $ isBeastMilk��5e      $
��Left=�6(   $
��10,3q�6C   $��5��6Q   $	��Top=�6^    5f      �� 5g      $
��Auto �6'   �� 5h      $
��Auto �6'    5j      �� 5k      $
��Auto �6$   .+�5m      �� $��Is Breast Milk:��6   $��0��62   $
��Left=�6B   $��3q�6]   $��0��6h   $��Center=�6u   �� 5n      -   5        $ isBreastMilkCheckBox��5n      $�� ��69   $��1��6D   $
��Left=�6T   $��3q�6�   $��0��6�   $��Center=�6�    6o   �� *IsBreastMilk+$	 TwoWay=�
 .  . 6�   5o      9� 5q      -   5         9�Name$ CommentsExpender��5q      $��Comments��61   $��6��6C   $��Stretch=�6P   $��10,30q�6n   .��5r        -   5        $ CommentsText��5r   ,    6B   �� *
Comments+$	 TwoWay=�5s      �� 5u      $��* = Required fields��6   $��7��63   $
��Bold"�6@   $��Center=�6R   �� 5v      -   5         ��Name$ AddChildButton��5v      $	��Add��6-   $��8��6;   $��0��6H   $��Stretch=�6X   $��16(�6v   $��10q�6�   ��5w      �� 5x       �� Triggers 5y      �� 5z       �� Binding 6*   �� $ firstNameTextBox��$
 Text� �� Value  6f   .a�5{   "   ��  �� Property � {� �� Value .  6E   5|      �� 5}       6*   �� $ 	idTextBox��$
 Text�  6_   .a�5~   "   ��  � {� .  6E   5      �� 5�       6*   �� $ motherIdComboBox��$ SelectedIndex� -16o   .a�5�   "   ��  � {� .  6E   5�      5�      5�      5�      5�      5�      