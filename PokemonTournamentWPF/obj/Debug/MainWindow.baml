   M S B A M L   `   `   `  ����O  KPokemonTournamentWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PokemonTournamentWPFPokemonTournamentWPF  5   	   %    PokemonTournamentWPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PokemonTournamentWPF     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Login��5	   	   $��431.978��6   $	��525��6(    � 
ResizeMode$ NoResize=�64   ��5
      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $	 Red��6   $ 1Q�6'   5      .��5      � #��� ��6    � ColumnDefinitions 5   
   �� 5      $
��331* �6   �� 5      $
��188* �6   .+�5   
   �� -   5         ��Name$ Titre��5      $��	Connexion��6   $��Center=�63   $��181,50,183,293q�6P   $��Center=�6h   $��48��6�   $	��155��6�   $��Times New Roman)�6�   $��32(�6�   $��2��6�   �� 5   
   -   5        $ label��5      $
��Left=�6   $��
59,124,0,0q�6:   $	��Top=�6N   $��Times New Roman)�6f   $	��102��6�   $��18.667(�6�   .��5      �� -   5        $ login��5      $��Login :��6#   $��34��65   $��92��6A   5      �� 5   
   -   5        $ password��5      $��
Password :��6"   $
��Left=�67   $��
59,198,0,0q�6R   $	��Top=�6f   $��Times New Roman)�6~   $��18.667(�6�   $	��124��6�   $��41��6�   �� 5   
   -   5        	 ��Name$		 log��5      $
��Left=�6   $��44��6:   $��240,124,0,0q�6F   
 �� TextWrapping$

 Wrap=�6[   $	��Top=�6o   $	��211��6�   $��Times New Roman)�6�   $��18.667(�6�   $��2��6�   G� 5   
   -   5         G�Name$
 pass��5      $
��Left=�6$   $��240,198,0,0q�6?   $	��Top=�6T   $��44��6l   $	��211��6x   $��Times New Roman)�6�   $��18.667(�6�   $��2��6�   �� 5   
   -   5         ��Name$ 
BConnecter��5      $��Se connecter��6%   $
��Left=�6<   $��180,300,0,0q�6W   $	��Top=�6l   $	��155��6�   $��34��6�   ���	#FF786666���	#FFF7793E$��2��6�   �� 5   
   -   5         ��Name$ avertissement��5      $
��Left=�6+   $��
59,258,0,0q�6F   $
��Wrap=�6Z   $�� ��6n   $	��Top=�6v   $	��392��6�   $��16.667(�6�   $	��red)�6�   $��36��6�   ���	#FFFD0000$��2��6�   �� 5   
   -	   5        $ adduser��5      $��Créer un compte��6"   $
��Left=�6<   $��181,349,0,0q�6W   $	��Top=�6l   $	��155��6�   ���	#FF786666���	#FFF7793E$��2��6�   $��34��6�   5       5!      