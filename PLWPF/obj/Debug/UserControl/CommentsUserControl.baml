   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5          PLWPF.CommentsUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:PLWPF     5      .��5
   
   �� .+�5      ]� ��. $��Stretch=�61   .+�5      �� -   5          ��Name$  AddCommentButton��5      $	��Add��63   $��15q�6B   $��40��6N   $	��100��6\   $��Right=�6k   $��20��6�   ��5      �� 5       �� Triggers 5      �� 5   "    �� Binding 6.   ��  �� ElementName$ CommentTextBox�� �� Path$
 Text� �� Value  6h   .a�5   &   ��  �� Property � {� �� Value .  6I   5   #   5      5      �� 5      -   5         ��Name$ CommentTextBox��5      $��10q�62   $��Stretch=�6>   $	��200��6\   $��50��6h   	 �� TextWrapping$
	 Wrap=�6w   $
��left=�6�   "
 �� VerticalScrollBarVisibility$
 Visible=�6�   
�������6�   5      �� 5   
   -   5         ��Name$ CommentsTextBlock��5      $
��10,4q�6R   $��Stretch=�6`   $	��260��6~   $
��Wrap=�6�   
���������6/   �� *Text+ �� Mode$ TwoWay=�6�   5      5      