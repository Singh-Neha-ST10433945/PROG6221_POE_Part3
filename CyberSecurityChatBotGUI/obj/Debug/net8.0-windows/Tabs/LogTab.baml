   M S B A M L   `   `   `  ����  CyberSecurityChatBotGUI)    #CyberSecurityChatBotGUI.Tabs.LogTab   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ���#E8F5E9.��5      �� $��10q�6   ���#E8F5E9.+�5   
   �� $��22(�6   $
��Bold"�6"   ���#2E8B57$��📚 Activity Log��5	      $��0,0,0,5q�6+   $��Center=�6<   �� 5
   
   $��14(�6   $t��nHere’s a quick glance at all your tasks, actions, and updates. Stay informed and keep track of your journey!��6"   
��� d �$��0,0,0,10q�5   +     �� TextAlignment$  Center=�6=   �� 5   
    �� Orientation$ Vertical=�6   $��Center=�6,   $��0,0,0,10q�6I   $	��300��6[   .+�5      ��  �� CornerRadius$ 5��6   
����������#A9D0A9$��1q�6O   $	��0,5q�6c   ._�5      �� -   5         ��Name$ 	SearchBox��5      $��30��6-   $��5q�69   
�������$��0q�5   -   $��	Search...��6A   5   8   5      �� 5      -   5        $��🔍 Search��5      $	��250��6)   $��30��65   $	��0,5q�6A   ���#4CAF50
�������$
��Bold"�5   L   ���#388E3C6^   �� 5      -   5        $��🔄 Refresh��5      $	��250��6*   $��30��66   $	��0,5q�6B   ���#4CAF50
�������$
��Bold"�5   M   ���#388E3C6_   �� 5      -   5        $��	❌ Clear��5      $	��250��6'   $��30��63   $	��0,5q�6?   ���#4CAF50
�������$
��Bold"�5   Q   ���#388E3C6c   5      �� 5    
   $
��0,10q�6   ���#BDBDBD6"   �� 5#   
   $ 5��6   ���#A9D0A9$��1q�68   
�������._�5$      &   System.Windows.Controls.DataGrid  -   5          Name$ LogDataGrid��5$         AutoGenerateColumns$ False��6,   $	��400��6H      AlternatingRowBackground
 �����   HeadersVisibility$ Column=�5&         CanUserAddRows$ False��5'      	   
IsReadOnly$
	 True��5(      
�������$��0q�5*      $��0,10,0,0q�5+      
   RowHeaderWidth$
 0Q�5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ ⏰ Time��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 69   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5/   )   �� *	Timestamp+ �� StringFormat$ {0:yyyy-MM-dd HH:mm:ss}��  50      $ 📝 Message��6)   $ * 6Y    6=   �� *	Message+6Y   52      53      �� 55   
   $(��"🌿 Stay curious and secure! 🌿��6   $��Italic$�6:   
��� d �$��0,10,0,0q�56      $��Center=�6&   57      58      