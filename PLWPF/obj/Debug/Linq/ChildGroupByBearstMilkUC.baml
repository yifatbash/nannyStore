   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5      $    PLWPF.ChildGroupByBearstMilkUC   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ SelectChildIdStackPanel��5      $��0��66   $
��Left=�6D   $��15q�6_   .+�5      �� $��Select nanny��6      subTitle#��[  6+   $��Center=�6M   $��3q�6j   $��Center=�6u   �� 5      -   5         ��Name$ nannyComboBox��5      $��Center=�6.   $��23��6K   $��3q�6W   $��Center=�6�   $��60��6�    �� SelectedItem 6b   �� *Nanny+ �� Mode$ TwoWay=� �� NotifyOnValidationError .  �� ValidatesOnExceptions . 6�   5      �� 5   
   $��1��6   .+�5      �� -   5         ��Name$ Header��5      $$��All Child Group By Bearst Milk��6(   $��Stretch=�6N    �� Padding$
 10,3q�6l   $��Stretch=�6{   ���	#FFFBF7F7
���   �$��21.333(�6�   $��2��6�   �� 5      -   5        	 ��Name$	 listView��5      $��2��6*   $��2��67   ��5      �� 5      .R�5      � 5      ��5      �� 5      .R�5      �� $��3q�6!   
��� ���$��5q�6J   ���	#FF130A0A._�5      �� .+�5   "   �� $��16(�6F   
�������$
��Bold"�6g   
 �� TextAlignment$
 Center=�6y   
���   ���6,   ��  �� Path$	 Key�6�   �� 5    "   $��Center=�6C   ��6+   �� ��5!   &   �� 5"   *   .R�5#   .   � 5$   +   5&   #   5'      5(      5)      5+      ��5,      �� 5-       �� Triggers 5.      �� 5/       �� Binding 6&   ��  �� ElementName$ nannyComboBox��$ SelectedIndex� �� Value -16h   .a�50      ��  �� Property � y� �� Value$ 	Collapsed=�6F   51      53      56      59      5:      