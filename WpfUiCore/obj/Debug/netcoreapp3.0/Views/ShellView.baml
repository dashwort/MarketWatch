   M S B A M L   `   `   `  ����D  @WpfUiCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfUiCore.ViewsWpfUiCore.Views  5   	       WpfUiCore.Views.ShellView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfUiCore.Views     5   	     � Title$  	ShellView��5   	   $	��750��6   $
��1200��6(    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$ ThreeDBorderWindow=�6.   ���	#FF3F3F46 J� OpacityMask
 �   � � 
ResizeMode$ NoResize=�5
   5   $y�Visible=�5   	   ���#36507e.��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��0.7* �6   �� 5      $
��0.3* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� -   5         ��Name$ FirstSubViewModel��5      $��1��5      $��1��5      $��1��5      
��� � �5      �� 5!   
   -   5        $ SecondSubViewModel��5"      $��1��5#      $��2��5$      5&      5'      