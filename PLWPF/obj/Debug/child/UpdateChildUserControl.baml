   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      "    PLWPF.UpdateChildUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      -   5           childBackground#��[  5
        � 	Resources  5      (   "PLWPF.BooleanToVisibilityConverter   BooleanToVisibilityConverter��%   &	         5   
   6-   5      .��5      �� $��Center=�6   ��.  �� PanningMode$ HorizontalOnly=�6O    J� SnapsToDevicePixels . $��Visible=�6�    � DataContext 6�   �� .��5   
   � $��Center=�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ title��5      $��Update Child��6#     
childTitle#��[ 6;   $��2��6_   $��0��6s   $��0,15,0,0q�6�   �� 5       -   5         ��Name$ SelectChildIdStackPanel��5       $��1��6:   $��0,10,0,0q�6G   .+�5!      �� $��Select child��6     subTitle#��[ 6/   $��Center=�6Q   $��3q�6n   $��Center=�6y   �� 5"      -   5         ��Name$ 
idComboBox��5"      $��Center=�6/   $��23��6L   $��3q�6X   $��Center=�6�   $��60��6�    �� SelectedItem 6c   �� *Id+	 �� Mode$	 TwoWay=�
 �� NotifyOnValidationError
 .  �� ValidatesOnExceptions . 6�   5#      � 5%      -   5         �Name$ childDetailsGrid��5%      $��2��6-   $��10,30q�6:    5&      �� 5'      $
��auto �6$   �� 5(      $
��auto �6$   �� 5)      $
��auto �6$   �� 5*      $
��auto �6$    � ColumnDefinitions 5-      �� 5.      $
��auto �6'   �� 5/      $
��auto �6'   .+�51      �� $��	Child Id:��6   $��0��6,   $
��Left=�6<   $��3q�6W   $��0��6b   $��Center=�6o   �� 52      -   5         ��Name$ childIdTextBlock��52      $��1��66   $
��Left=�6F   $��23��6a   $��3q�6m   $��0��6x   $��Center=�6  $	��120��6   ��6�   �� *Id+$	 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�
 .  . 6   �� 53      $��
Mother Id:��6   $��0��6-   $
��Left=�6=   $��3q�6X   $��1��6c   $��Center=�6p   �� 54      -   5        $ motherIdTextBlock��54      $��1��67   $
��Left=�6G   $��23��6b   $��3q�6n   $��1��6y   $��Center=�6  $	��120��6'  ��6�   �� *
MotherId+$	 TwoWay=�$ PropertyChanged=�
 .  . 6'  �� 55      $��* First Name:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��2��6f   $��Center=�6s   �� 56      -   5         ��Name$ firstNameTextBox��56      $��1��64   $
��Left=�6D   $��23��6_   $��3q�6k   $��2��6v   $��Center=�6
  $	��120��6%  ��6�   �� *	FirstName+$	 TwoWay=�$ PropertyChanged=�
 .  . 6%  �� 57      $��* Birth Date:��6   $��0��60   $
��Left=�6@   $��3q�6[   $��3��6f   $��Center=�6s   (  "System.Windows.Controls.DatePicker  58      -	   5          Name$ birthDateDatePicker��58      $��1��6:   $
��Left=�6J   $��3q�6e   $��3��6p   $��Center=�6     SelectedDate 6}   �� *	BirthDate+$	 TwoWay=�$ PropertyChanged=�
 .  . 6  ��5:      �� 5;       �� Triggers 5<      �� 5=       �� Binding 6*   ��  �� ElementName$ 
idComboBox�� �� Path$ SelectedIndex� �� Value -16i   .a�5>   "   ��  �� Property � {� �� Value .  6C   5?      5A      5C      �� 5E      -
   5        $ moreStackPanel��5E      $��3��62   $
��10,0q�6?   ��5F      �� 5G       5H      �� 5I       6*   �� $ 
idComboBox��$ SelectedIndex� -16i   .a�5J   "   ��  � {� .  6I   5K      5M      .+�5P      � -   5        $ isSpecialNeedsChildGrid��5P      $
��Left=�68   $	��Top=�6S    5Q      �� 5R      $
��Auto �6+   �� 5S      $
��Auto �6+    5U      �� 5V      $
��Auto �6(   �� 5W      $
��auto �6(   .+�5Y      �� $��Is Special Needs Child:��6   $��0��6>   $
��Left=�6N   $��3,0,0,3q�6i   $��0��6z   $��Bottom=�6�   �� 5Z      -   5         ��Name$! isSpecialNeedsChildCheckBox��5Z      $�� ��6D   $��1��6O   $
��Left=�6_   $��3q�6  $��0��6  $��Center=�6(   d� 	IsChecked 6z   �� *IsSpecialNeedsChild+$	 TwoWay=�$ PropertyChanged=�
 .  . 6C  %   PLWPF.UpdateCommentsUserControl  5[      -   5          Name$ SpecialNeedsText��5[   6   $��1��6{   $��2��6�      Text 6P   �� *SpecialNeeds+$	 TwoWay=�y�6�   �� $! isSpecialNeedsChildCheckBox��$ 	IsChecked� �� 	Converter# [ 5\      � 5^      -   5        $ isFoodAllergyGrid��5^      $
��Left=�62   $	��Top=�6M    5_      �� 5`      $
��Auto �6+   �� 5a      $
��Auto �6+    5c      �� 5d      $
��Auto �6(   �� 5e      $
��Auto �6(   .+�5g      �� $��Is Food Allergy:��6   $��0��67   $
��Left=�6G   $��3q�6b   $��0��6m   $��Center=�6z   �� 5h      -   5        $ isFoodAllergyCheckBox��5h      $�� ��6>   $��1��6I   $
��Left=�6Y   $��3,0,0,8q�6�   $��0��6�   $��Bottom=�6�    6t   �� *IsFoodAllergy+$	 TwoWay=�
 .  . 6    5i      -   5        $ FoodAllergyText��5i   6   $��1��6y   $��2��6�    6O   �� *FoodAllergy+$	 TwoWay=�y�6�   �� $ isFoodAllergyCheckBox��$ 	IsChecked�# [ 5j      � 5l      -   5        $ isMedicinesAllergyGrid��5l      $
��Left=�67   $	��Top=�6R    5m      �� 5n      $
��Auto �6+   �� 5o      $
��Auto �6+    5q      �� 5r      $
��Auto �6(   �� 5s      $
��Auto �6(   .+�5u      �� $��Is Medicines Allergy:��6   $��0��6<   $
��Left=�6L   $��3q�6g   $��0��6r   $��Center=�6   �� 5v      -   5        $  isMedicinesAllergyCheckBox��5v      $�� ��6C   $��1��6N   $
��Left=�6^   $��3,8,0,0q�6�   $��0��6�   $	��Top=�6   6y   �� *IsMedicinesAllergy+$	 TwoWay=�
 .  . 6    5w      -   5        $ MedicinesAllergyText��5w   6   $��1��6�   $��2��6�    6T   �� *MedicinesAllergy+$	 TwoWay=�y�6�   �� $  isMedicinesAllergyCheckBox��$ 	IsChecked�# [ 5x      � 5z      -   5        $ isBeastMilk��5z      $
��Left=�6,   $
��10,3q�6G   $��6��6U   $	��Top=�6b    5{      �� 5|      $
��Auto �6+   �� 5}      $
��Auto �6+    5      �� 5�      $
��Auto �6(   .+�5�      �� $��Is Breast Milk:��6   $��0��66   $
��Left=�6F   $��3q�6a   $��0��6l   $��Center=�6y   �� 5�      -   5        $ isBreastMilkCheckBox��5�      $�� ��6=   $��1��6H   $
��Left=�6X   $��3q�6�   $��0��6�   $��Center=�6�    6s   �� *IsBreastMilk+$	 TwoWay=�
 .  . 6�   5�      9� 5�      -   5         9�Name$ commentsExpander��5�      $��Comments��65   $��7��6G   $��Stretch=�6T   $��10,30q�6r   .��5�        -   5        $ CommentsText��5�   6   $��1��6s   $��2��6�    6L   �� *
Comments+$	 TwoWay=�6�   5�      5�      �� 5�      $��* = Required fields��6   $��7��63   $
��Bold"�6@   $��Center=�6R   �� 5�      -   5          ��Name$  UpdateChildButton��5�      $��Update��60   $��8��6A   $��0��6N   $��Stretch=�6^   $��16(�6|   $��10q�6�   ��5�      �� 5�       5�      �� 5�       6*   �� $ 
idComboBox��$ SelectedIndex� -16i   .a�5�   "   ��  � {� .  6E   5�      �� 5�       6*   �� $ firstNameTextBox��$
 Text�  6f   .a�5�   "   ��  � {� .  6E   5�      5�      5�      5�      5�      5�      