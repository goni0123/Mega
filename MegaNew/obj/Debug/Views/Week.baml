   M S B A M L   `   `   `  ����?  ;Mega, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MegaNew.ViewsMegaNew.Views  5          MegaNew.Views.Week   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:MegaNew.Views     5      
�������.��5	      � $
��1690��6   $
��1080��6   .+�5
   
   (  "System.Windows.Controls.DatePicker  -   5           Name$
  From��5
      $��1��6#   $
��Left=�60   $��647,100,0,0q�6K   $	��Top=�6`   $	��150��6x   $��40��6�   $��20(�6�     5   
   -   5        $  To��5      $��2��6!   $
��Left=�6.   $��863,100,0,0q�6I   $	��Top=�6^   $��20(�6v   $	��150��6�   $��40��6�   �� 5   
   $
��From��6   $
��Left=�6   $��586,102,0,0q�6:   $	��Top=�6O   
���   �$��20(�6z   �� 5   
   $��To��6   $
��Left=�6   $��823,102,0,0q�68   $	��Top=�6M   
���   �$��20(�6x   �� 5   
   -   5        $��Extract��5      $
��Left=�6#   $��1031,102,0,0q�6>   $	��Top=�6T   $��37��6�   $��59��6�   �� 5   
   $
��Week��6   $
��Left=�6   $��708,164,0,0q�6:   $	��Top=�6O   
���   �$��20(�6z   �� 5   
   -   5         ��Name$ Weeks��5      $��3��6!   $
��Left=�6.   $��778,164,0,0q�6I    �� TextWrapping$
 Wrap=�6^   $	��Top=�6s   $	��140��6�   $��40��6�   $��20(�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ weekData��5      $��629,242,571,338q�6&      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Week Number��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *	Week_id+6V     5      $ Nalog Number��6%   $	 150 6X    6;   �� *
Nalog_nr+6X   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Check��6)   $	 150 6V    68   �� *	Check_inl+6V   5      �� 5   
   -   5        $��Delete��5      $
��Left=�6"   $��1099,102,0,0q�6=   $	��Top=�6S   $��37��6�   $��58��6�   �� 5   
   -   5        	 ��Name$	 Search��5      $��Search��6!   $
��Left=�62   $��924,164,0,0q�6M   $	��Top=�6b   $��40��6�   $��75��6�   �� 5   
   -   5        $
	 Last��5      $��	Last Week��6   $
��Left=�63   $��1004,164,0,0q�6N   $	��Top=�6d   $��86��6�   $��40��6�   �� 5   
   $��Weeks��6   $��Center=�6!   $��0,10,0,0q�6>   $	��Top=�6P   
���   �$	��133��6{   $��36(�6�   $��Segoe UI Black)�6�   $��63��6�     5   
   -	   5        $ we��5      $��474,242,1061,338q�6     .   5        5      $
 Week��6%   $	 150 6O    63   �� *	Week_id+6O   5       5!      5"      