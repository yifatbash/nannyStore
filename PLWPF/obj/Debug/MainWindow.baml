   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5   	       PLWPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:PLWPF     5   	     � Title$  
MainWindow��5   	   $	��700��6   $	��900��6)   
���   � � 	Resources 5
      ��%    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� -   5         ��Name$ title��5      $$��Welcome To Our Nanny DataBase!��6   $��0��6I    	  title#��[  6W   
�������$��36(�6�   $��15q�6�   $��Center=�6�   �� 5   
   -   5        $ subtitle��5      $��Choose Category:��6"   $��1��6=   #��[  6J   
�������$��20q�6�   $��Center=�6�   �� 5   
   $&�� /PLWPF;component\Images\baby.png��6   $	��150��6:   $��0��6F   $��1��6S   $��2��6c   $��
0,15,50,15q�6t   $��Right=�6�   �� 5    
   -   5         ��Name$ NannyButton��5       $��Nanny��6&   $��2��66   $��2��6D   $��30(�6X   $��10q�6f     NannyButton#��[ 6r   6�   �� 5!   
   -   5        $ MotherButton��5!      $��Mother��6'   $��3��68   $��2��6F   $��30(�6Z   $��10q�6h     MotherButton#��[ 6t   6�   �� 5"   
   -   5        $ ChildButton��5"      $��Child��6&   $��4��66   $��2��6D   $��30(�6X   $��10q�6f     ChildButton#��[ 6r   6�   �� 5#   
   -   5        $ ContractButton��5#      $��Contract��6)   $��5��6<   $��2��6J   $��30(�6^   $��10q�6l     ContractButton#��[ 6x   6�   �� 5$   
   -   5        $ 
LinqButton��5$      $
��Linq��6%   $��6��64   $��2��6B   $��30(�6V   $��10q�6d     
LinqButton#��[ 6p   6�   5%      5'      