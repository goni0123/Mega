   M S B A M L   `   `   `  ����?  ;Mega, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:MegaNew.ViewsMegaNew.Views  5          MegaNew.Views.Outgoing   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:MegaNew.Views     5      $
��1000��5      $
��1690��6   
�������.��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Search��6   $��15(�6"   $��0��60   $��680 10 960 10q�6=   �� 5   
   -   5         ��Name$ search��5      $��0��6"   $��10q�6/   $	��200��6;   $��30��6G   $	��Top=�6S   ��6k   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   -   5         ��Name$	 Add��5      $��0��6   $��1600,10,0,10q�6+   $��80��6A   $	��Add��6L   $
��Left=�6�   ��6Z   �� *
AddCommand+��6y   ��  �� ElementName$ nalog�� �� Path$
 Text�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	NalogData��5      $��1��6&   $��10,10,10,55q�63      AutoGenerateColumns .     CanUserAddRows .  $��2��6�   ��6{   �� *OutgoingData+	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Nalog��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *	NalogNr+6P     5      $
 Truck��6%   $
 Auto 6N    64   �� *Truck+6N     5      $	
 RIT��6%   $
 Auto 6J    62   �� *Rit+6J     5      $
 Invoice��6%   $
 Auto 6R    66   �� *	Invoice+6R     5      $
 Driver��6%   $
 Auto 6P    65   �� *Driver+6P   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $
 Check��6)   $
 Auto 6R    68   �� *Check+6R   4  .System.Windows.Controls.DataGridTemplateColumn  5      $	 150 6)      CellTemplate 5       �� 5!      .R�5"      �� -   5        $��Update��5"   %   
 � Tag 6N   �� *	NalogNr+5#      5%        5'      $	 150 6)    5(      �� 5)      .R�5*      �� $��Delete��6%   ��66   �� *DataContext.DeleteCommand+ �� RelativeSource ��  �� AncestorType"  ��6�   �� *	NalogNr+5+      5-        5/      $	 150 6)    50      �� 51      .R�52      �� $��Print��6%   ��65   �� *DataContext.PrintCommand+ �� "  ��6�   �� *	NalogNr+53      55      57      �� 58   
   $��Nalog��6   $��15(�6!   $��0��6/   $��1330,10,310,10q�6<   �� 59   
   -   5        $ nalog��59      $��1395,0,95,0q�6!   $	��200��66   $��30��6B   $��Center=�6N   5:      5;      