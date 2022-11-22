
public class HelperStats : FriendStats{
  
    public override void SetKills(HeroType killed){
    
        if (GameManager.gameOver) { return; }

        if (killed == HeroType.Enemy){

            ++kills;

            ++gameManager.killsByFriends;
          
            gameManager.UpdateTopLabels();

        }

    }
}
