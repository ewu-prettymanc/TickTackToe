   M S B A M L   `   `   `  ����  TickTackToe    TickTackToe.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  	TTTWindow��5   	    � Title$ Tick Tack Toe��5   	   $	��550��6   $	��520��6,   ���	#FF1D0000���	#FF912F0D � WindowStartupLocation$ CenterScreen=�6�   
���   �$	��517��6�   $	��550��6   � Icon$ ttticon.ico��6  .��5      � -   5         �Name$ TTTWindowGrid��5      $��-1,0,-0.6,-1.2q�6"   ���	#FFBE451C � RowDefinitions 5   
   �� 5      $
��183* �6   �� 5      $	��80* �6   �� 5	      $	��75* �6   �� 5
      $
��183* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��5* �6   �� 5      $��4* �6   .+�5   
   o� -   5         o�Name$
 Menu��5      $��3��6   $	��Top=�61   ���	#FF912F0D.@�5      m� -   5         m�Name$ FileMenuItem��5      $��_File��6-   ���	#FF912F0D���	#FF1D0000$��10,0,0,0q�6j   .@�5      m� -   5        $ NewGameMenuItem��5      $��_New Game -Regular Board-��64   ���	#FFBE451C���	#FF912F0D6�   m� 5      -   5        $ NewGameWithSizeMenuItem��5      $��New  Game -_Board Select-��6<   ���	#FFBE451C���	#FF912F0D6�   m� 5      -   5        $ CloseMenuItem��5      $��_Close��62   ���	#FFF70000���	#FF912F0D6u   5      m� 5      -   5        $ HelpMenuItem��5      $��_Help��6-   ���	#FF912F0D���	#FF1D0000.@�5      m� -	   5        $ UsageMenuItem��5      $��_Usage��62   ���	#FFBE451C���	#FF912F0D6u   m� 5      -
   5        $ AboutMenuItem��5      $��_About��62   ���	#FFBE451C���	#FF912F0D6u   5      5      �� 5   
   -   5        	 ��Name$	 	TTTCanvas��5      $��60,75,60,50q�6$   $��3��69   $��4��6M   ���	#FF615B5B$	��400��6u   $	��400��6�   
 J� RenderTransformOrigin$
 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� 5!      T� 5"      5#      .+�5%      �� -   5         ��Name$ cmbxBoardSize��5%       �� Left$ 74��6.   
 �� Top$	 190��6?    J� 	AllowDrop . $��Center=�6a   $��Center=�6�   $��5,5,5,7q�6�   $
��Left=�6�   $��Center=�6�   $	��100��6�   $��28��6�    �� 
IsEditable .  �� Text$ 3��6  $y�Hidden=�6"   � BindingGroup 5&      &   System.Windows.Data.BindingGroup  5'      .@�5)      �� $��1��6   6-   �� 5*      $��2��6   6-   �� 5+      $��3��6   6-   �� 5,      $��4��6   6-   �� 5-      $��5��6   6-   �� 5.      $��6��6   6-   �� 5/      $��7��6   6-   �� 50      $��8��6   6-   �� 51      $��9��6   6-   �� 52      $��10��6   6.   53      �� 54      -   5         ��Name$ 	btnSubmit��54      $��Submit��6(   $	 226��69   $	 191��6K   $��80��6\   $��28��6g   $y�Hidden=�6�   $
��Left=�6�   $��Center=�6�   55      �� 56   
   -   5        $ btnCoinToss��56      $��
_Coin Toss��6&   $
��Left=�6;   $��1,145.6,0,5q�6V   $��3��6k   $��75��6x   $��1��6�   $��28��6�   {�.  
���   ����	#FFE2E2E2 J� Opacity$	 0.3Q�6�   $��Center=�6  $	��1,5q�6'  $��Bottom=�65  �� 57   
   -   5        $ btnPlayAgain��57      $��_Play Again��6'   $��1��6=   $��Right=�6M   $��124,144.6,0,5q�6i   $��3��6�   $��75��6�   $��29��6�   {�.  
���   ����	#FFE2E2E2$	 0.3Q�6�   $��Bottom=�6	  �� 58   
   -   5         ��Name$ lblGameStatus��58      $��Tick Tack Toe��6'   $��
60,29,62,0q�6?   $
 1,0.5&�6S   $	��Top=�6q   $��Comic Sans MS)�6�   $��18(�6�   $��Stretch=�6�   ���	#FF912F0D���	#FF1D0000$��3��6  $��Center=�6  59      5:      