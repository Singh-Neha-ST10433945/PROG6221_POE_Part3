   M S B A M L   `   `   `  ����  CyberSecurityChatBotGUI*    $CyberSecurityChatBotGUI.Tabs.QuizTab   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ���#E8F5E9.��5
      �� $
��Auto=�6   .��5   
   �� $��15q�6   
�������  �� Padding$  15q�60   ���#A9D0A9$��1q�6S    �� CornerRadius$ 12��6g   ._�5      �� -   5         ��Name$ 	MainPanel��5      $��Center=�6,   $	��Top=�6I   $	��600��6a   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $��0,0,0,10q�6S   .+�5      �� $��22(�6    $
��Bold"�6.   ���#2E8B57$"��🧠 Cybersecurity Quiz Game��5       5      �� 5      $��14(�6   $��SemiBold"�6*   
��� d �$��Center=�5      $A��;Test your cybersecurity knowledge and boost your awareness!��69   $��0,0,0,15q�6|   �� 5      $
��0,10q�6   ���#BDBDBD6*   �� 5      -   5        $ 
StartPanel��5      $��Center=�61   $y�Visible=�6N   $��0,0,0,10q�6c   .+�5      �� $U��OYou'll face 10 quick questions. Each one is timed, so think fast but smart! ⏱��6    $��14(�5        �� TextAlignment$ Center=�6.   $��SemiBold"�6E   �� 5      $��Click Start to begin 🚀��6    $��14(�6?   $
��0,10q�6M   $ Center=�6[   $��SemiBold"�6r   �� 5      -   5         ��Name$ StartButton��5      $��🚀 Start Quiz��62   $	��200��6J   $��35��6V   ���#4CAF50
�������$
��Bold"�5   E   ���#388E3C6q   5       �� 5#      -   5        $ QuestionPanel��5#      $y�	Collapsed=�64   $
��0,10q�6K   .+�5$      �� -   5         ��Name$ QuestionText��5$       $��16(�66   $
��Bold"�6D   $
��Wrap=�6V   $
��0,10q�6j   �� 5%      -   5        $ OptionsPanel��5%   !   $	��0,5q�67   �� 5&      -   5        $ FeedbackBlock��5&       $��5q�67   $
��Bold"�6B   $
��Wrap=�6T   �� 5'      -   5        $ CurrentScoreText��5'       $��14(�6:   
����  �$��SemiBold"�6^   $��0,5,0,0q�6t   $ Right=�6�   �� 5(      -	   5        $ 
NextButton��5(      $
��Next��61   $	��100��6@   $��30��6L   $��5q�6X   ���#8FBC8F
�������$
��Bold"�5)   E   $y�	Collapsed=�6r   5*      �� 5-      -
   5        $ ProgressBarContainer��5-      $ 
Horizontal=�6;   $��Center=�6T   $
��0,10q�6o   $y�	Collapsed=�6}   .+�5.      � -   5         �Name$ ProgressBar��5.   "   $	��200��67   $��10��6C    � Minimum$ 0Q�6O   	 � Maximum$		 100Q�6[   �� 5/      -   5        $ ProgressLabel��5/       $��10,0,0,0q�67   $��Center=�6I   50      �� 53      -   5        $ TimeText��53      $��14(�6.   
���  ��$
��Bold"�6M   $	��0,5q�6_   $ Center=�54      $y�	Collapsed=�63   �� 57      -   5        $ ReviewPanel��57      $y�	Collapsed=�62   $��10q�6I   .+�58      �� -   5        $ 	ScoreText��58       $��18(�63   $
��Bold"�6A   $��5q�6S   $ Center=�6^   �� 59      -   5        $ FinalMessage��59       $��14(�66   $��5q�6D   $ Center=�6O   �� 5:      -   5        $ ComparisonText��5:       $��14(�68   $��5q�6F   $ Center=�6Q   �� 5;      $ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   .+�5<      �� -   5        $ ReviewAnswersButton��5<   !   $��📋 Review Answers��6>   $	��150��6Z   $��30��6f   $��5q�6r   ���#8FBC8F
�������$
��Bold"�5=   I   6[   �� 5>      -   5        $ TryAgainButton��5>   !   $��🔄 Try Again��69   $	��150��6P   $��30��6\   $��5q�6h   ���#4CAF50
�������$
��Bold"�5?   I   6[   5@      5A      �� 5D      -   5        $ ReviewAnswersList��5D      $y�	Collapsed=�68   $��10q�6O   .+�5E      �� $"��📚 Review of Your Answers:��6    $��16(�6B   $
��Bold"�6P   $��5q�6b   5F      �� 5H      $A��;🌟 Keep learning and improving your cybersecurity skills!��6   $��Italic$�5I      
��� d �$��10q�6F   $��Center=�6R   5K      5L      5M      5N      