   M S B A M L   `   `   `  ����?  ;Mega, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MegaNew.ViewsMegaNew.Views  5          MegaNew.Views.ComboEdit   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:MegaNew.Views     5      $
��1080��5      $
��1690��6-   .��5      � 
�������.+�5	   
   �� -   5          ��Name$	  Add��5	      $	��Add��6   $
��Left=�6-   $��554,681,0,0q�6H   $	��Top=�6]   $��40��6�   $��60��6�   �� 5
   
   $��Truck��6   $
��Left=�6    $��254,671,0,0q�6;   $	��Top=�6P   
���   �$��36(�6{   �� 5   
   -   5         ��Name$ Truck��5      $
��Left=�6!   $��369,675,0,0q�6<    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6f   $	��155��6~   $��50��6�   $��16(�6�   $��1��6�   �� 5   
   -   5        $  Delete��5      $��Delete��6"   $
��Left=�63   $��637,680,0,0q�6N   $	��Top=�6c   $��40��6�   $��60��6�   �� 5   
   -   5        $  Add_trailor��5      $	��Add��6'   $
��Left=�65   $��1165,681,0,0q�6P   $��40��6f   $	��Top=�6r   $��60��6�   �� 5   
   $��Trailor��6   $
��Left=�6"   $��865,677,0,0q�6=   $	��Top=�6R   
���   �$��36(�6}   �� 5   
   -   5        $ Trailor��5      $
��Left=�6#   $��980,676,0,0q�6>   $
 Wrap=�6S   $	��Top=�6h   $	��155��6�   $��50��6�   $��16(�6�   $��2��6�   �� 5   
   -   5        $  Delete_t��5      $��Delete��6$   $
��Left=�65   $��1248,681,0,0q�6P   $	��Top=�6f   $��40��6�   $��60��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$	 Tru��5      $��310,110,1075,440q�6    $��25(�6:      AutoGenerateColumns .     CanUserAddRows .  ��6{   �� *	TruckData+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5         FontSize$ 25Q�6%   ,  &System.Windows.Controls.DataGridColumn   Header$ Truck��63   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 Auto 6\   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6B   �� *Truck+6\   5        5   
   -   5        $	 Tra��5      $��905,110,480,440q�6    $��25(�69    .   .  ��6z   �� *TrailorData+ 5        5      $ 25Q�6%   $ Trailor��63   $
	 Auto 6`   
 6D   �� *	Trailor+6`   5      5      5      