   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      L HWordGuessGame_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WordGuessGame_WPF.ViewsWordGuessGame_WPF.Views 5      /   )WordGuessGame_WPF.Views.WordGuesserWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      7local%clr-namespace:WordGuessGame_WPF.Views     5        � Title$  WordGuesserWindow��5	      $	��700��6,   $	��750��69   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��400 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Wordguesser��6   $��0��6'   $��24(�5      $
��Bold"�6"   $��Center=�64   $��20q�6Q   � 5   
   -   5         �Name$ 	GuessGrid��5      $��1��6"   $��Center=�6/   .+�5      �� ��6   �� *GuessTextBoxes+��5      �� 5      .R�5      A�  A� Rows 6&   �� *CurrentGame.TurnsAmount+ A� Columns 6O   �� *CurrentGame.WordLength+5      ��5      �� 5      .R�5       �� $��1q�6!   
�������$��5q�6M   ._�5!      �� $��24(�6&   $��50��64   $��50��6?    �� TextAlignment$ Center=�6K   $��Center=�6b    �� 	MaxLength$ 1��5"      $��Center=�6-   $��Center=�6Q   
���   �
���   � �� TextChanged 5$      �� *TextBoxTextChangedHandler+5%      5&      5(      5)      �� 5+   
   -   5         ��Name$ SubmitButton��5+      $��Submit guess��6'   $��2��6>   $��0,10,0,0q�6K   $��Center=�6]   $��Center=�6z   $��SubmitGuess��5,      ���	#FF0078D4��6	   �� {�5-   	   �� *IsSubmitButtonEnabled+��5.      �� 5/      	 �� 
TargetType"	 ��6   .�50      �� 
 �� Property
 � �� �� Value
 �����63   �� 51      
 � �� �	#FF0078D464   �� 52      
 � ��$ 2q�68   �� 53      
 � ��$ 10q�60   �� 54      
 � ��$ 16(�61   �� 55      
 � ��$	 150��6.   �� 56      
 � ��$ 50��6/   �� 57      
 � y�$ Visible=�63   �� 58      
 � �� 59      �� 5:       �� 
TargetType" ��6.   .R�5;   "   �� #��z��6)   #��z��5<      #��z��5=       �� CornerRadius$ 3��5>      ._�5?   &   �� $��Center=�67   $��Center=�5@   %   $
��True��5A   %   5B   #   5C      5E       �� Triggers 5F      P� 5G       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5H      �� 
 � �� �	#FF0056A06;   �� 5I      
 � �� �	#FF0056A06<   5J      �� 5K       �� Binding 6&   �� *IsGameFinished+ �� Value True6I   .a�5L      �� 
 � y�$ 	Collapsed=�6;   5N      �� 5O       6&   �� *IsSubmitButtonEnabled+ True6P   .a�5P      �� 
 � ��
 ��ح�6;   5Q      5S      5U      �� 5W   
   -   5        $ PlayNewGameButton��5W      $��Play New Game��6,   $��3��6D   $��0,10,0,0q�6Q   $��Center=�6c   $��Center=�6�   $��StartNewGame��5Y   %   
��� � �y�5X      �� *IsGameFinished+    BooleanToVisibilityConverter �� 	Converter# [  ��5Y      �� ��5Z      �� 5[      "	 ��6   .�5\      �� 
 � ��
 �����63   �� 5]      
 � ��
 � � �64   �� 5^      
 � ��$ 2q�68   �� 5_      
 � ��$ 10q�60   �� 5`      
 � ��$ 16(�61   �� 5a      
 � ��$	 150��6.   �� 5b      
 � ��$ 50��6/   �� 5c      
 � �� 5d      �� 5e      " ��6.   .R�5f   "   �� #��z��6)   #��z��5g      #��z��5h      $ 3��5i      ._�5j   &   �� $��Center=�67   $��Center=�5k   %   $
��True��5l   %   5m   #   5n      5p      5q      5s      �� 5u   
   $��4��6   $��18(�6B   $
��Bold"�6P   $��Center=�6b   $��20q�6   ��6!   �� *StatusTextBlock+6   5v      5w      