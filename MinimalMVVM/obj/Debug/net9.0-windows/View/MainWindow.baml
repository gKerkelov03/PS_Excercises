   M S B A M L   `   `   `  ����F  BMinimalMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MinimalMVVM.ViewModelMinimalMVVM.ViewModel  5   	   !    MinimalMVVM.View.MainWindow   5   	   1	ViewModel#clr-namespace:MinimalMVVM.ViewModel  5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  
MainWindow��5    	   $	��366��6   $	��300��6)    � DataContext 5"      %   MinimalMVVM.ViewModel.Presenter  5#   
    J� InputBindings 5%      �� 5&   
   
 �� Key$ Enter��6    �� Command 6!   �� *ConvertTextToUpperCommand+.��5)      � $��5q�6    � RowDefinitions 5*   
   �� 5+      $
��Auto �6   �� 5,      $
��Auto �6   �� 5-      $
��Auto �6   �� 5.      $��* �6   �� 5/      $
��Auto �6   �� 50      $
��Auto �6   .+�53   
   �� 
����  �$��0��6"   .��6/   Text To Convert6@   �� 54   
   $��1��6Q   $��0,5,0,5q�6^   ��6   �� *
SomeText+ �� UpdateSourceTrigger$ PropertyChanged=�6^   �� 56   
   
����  �$��2��6"   .��6/   	History68   �� 57   
   $��3��62   $��0,5,0,5q�6?   ��6   �� *	History+6?   �� 59   
   $��4��6?   $��0,5,0,5q�6L   ��6   �� *ConvertTextToUpperCommand+.��6]   
To Upper6g   �� 5:   
   $��5��6?   $��0,5,0,5q�6L   ��6   �� *ConvertTextToLowerCommand+.��6]   
To Lower6g   5;      5<      