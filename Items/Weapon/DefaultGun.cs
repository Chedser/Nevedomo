
using System.Collections;
using UnityEngine;

public class DefaultGun : WeaponRaycast, IShootable{

    private void Awake(){

        health = GetComponentInParent<IDemagable>();
        canShoot = true;

    }

    // Update is called once per frame
    void Update(){

        if (health.GetCurrentHealth() <= 0 || 
            GameManager.gameOver || 
            GameManager.isPaused) { return; }
      
        if (canShoot && Input.GetMouseButtonDown(0)){

            Use();

            canShoot = false;

        }
    }

    protected override void Use(){
     
            Shoot();

            AwakeEnemies();

    }

    public void Shoot(){

        Vector3 directionRay = aim.transform.position - missilePlace.position;

        RaycastHit hit;

        if (Physics.Raycast(missilePlace.position, directionRay, out hit, maxKillDistance)){

            GameObject hitGo = hit.collider.gameObject;
            Rigidbody hitRb = hitGo.GetComponent<Rigidbody>();

            if (hitGo.GetComponent<Rigidbody>()){

                if (hitGo.GetComponent<SpawnInfo>()) { // ѕопали по врагу

                    if (hitGo.GetComponent<IDemagable>().GetCurrentHealth() > 0) {

                        SetDemage(hit);
                    }

                    if (hitGo.GetComponent<RigidbodyInfo>().rigidbodyType == RigidbodyType.Flesh)
                    {

                        ShowBloodImpact(hit);

                    }
                    else {

                        ShowBulletImpact(hit);
                        TryToExplode(hitGo);
                        PlaySoundImpact(hit);

                    }
                 
                }else{ // ѕопали по твердому телу

                    DestroyFixedJoint(hitGo);

                    AddForceToBody(hitRb, directionRay, thrustToMove);

                    ShowBulletImpact(hit);

                    TryToExplode(hitGo);
                    PlaySoundImpact(hit);

                    TrySetBallOwner(hitGo);
                    
                }

            }
           
        }

        StartCoroutine(CoroutineShoot());

    }

    public IEnumerator CoroutineShoot(){

        // небольша€ задержка
        yield return new WaitForSeconds(shootTime);
        // если еще не все выстрелы произвели...
        if (bulletsToGo > 0){

            weaponController.canShoot = false;

            if (!shootSound.isPlaying) { shootSound.Play(); }
            shootEffect.Play();

            // то уменьшаем нашу переменную
            bulletsToGo--;
            // и еще раз стрел€ем
            Use();

        }
        else{// если все выстрелы уже произвели...
         // то небольша€ задержка
            yield return new WaitForSeconds(shootTime);

            weaponController.canShoot = true;

            shootSound.Stop();
            shootEffect.Stop();
        
            bulletsToGo = bulletsToGoInit;

            canShoot = true;

            // выходим с сопрограммы
            yield break;

        }

    }

 protected override  void SetDemage(RaycastHit hit) {

        if (hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() <= 0) { return; }

        hit.collider.gameObject.GetComponent<IDemagable>().TakeDemage(demage, HeroType.Player);

        if (hit.collider.gameObject.GetComponent<IDemagable>().GetCurrentHealth() <= 0){

            stats.SetKills(hit.collider.gameObject.GetComponent<SpawnInfo>().heroType);

        }
        else if(hit.collider.gameObject.GetComponent<AI>() && hit.collider.gameObject.GetComponent<AI>().canBeAwaked) {

            hit.collider.gameObject.GetComponent<AI>().aim = GetComponentInParent<SpawnInfo>().gameObject;
            hit.collider.gameObject.GetComponent<AI>().state = State.MoveToAim;

        }

      //  if (hit.collider.gameObject.GetComponent<Health>().CurrentHealth <= 0) { playerStats.SetKills(); }

    }

 protected override   void ShowBulletImpact(RaycastHit hit) {

        GameObject goPr = GetBulletImpact(hit);

        GameObject obj = Instantiate(goPr, hit.collider.transform.position, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();
        

    }

 protected override GameObject GetBulletImpact(RaycastHit hit) {

        GameObject goPr = bulletImpactGroundPr;

        if (hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>())
        {

            RigidbodyType rigidbodyType = hit.collider.gameObject.GetComponentInParent<RigidbodyInfo>().rigidbodyType;

            switch (rigidbodyType)
            {

                case RigidbodyType.Iron: goPr = bulletImpactIronPr; break;
                case RigidbodyType.Sand: goPr = bulletImpactSandPr; break;
                case RigidbodyType.Wood: goPr = bulletImpactWoodPr; break;

            }

        }

        return goPr;

    }

  protected override  void ShowBloodImpact(RaycastHit hit){
   
        GameObject obj = Instantiate(bloodImpactPr, hit.point, Quaternion.identity);
        obj.transform.SetParent(hit.collider.transform);
        obj.transform.position = hit.point + hit.normal * 0.01f;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.transform.rotation = Quaternion.FromToRotation(obj.transform.up, hit.normal);
        obj.GetComponent<ParticleSystem>().Play();
        Instantiate(soundImpactFlesh, hit.point, Quaternion.identity);
           
    }

  protected override  void AddForceToBody(Rigidbody rb, Vector3 dir, float thrust) {

        /* if (rb.gameObject.GetComponent<FootballCollision>()) {
            rb.gameObject.GetComponent<FootballCollision>().DropBall();
        } */

        rb.AddForce(dir * thrust, ForceMode.Impulse);

    }

 protected override void DestroyFixedJoint(GameObject hitGo) {

        FixedJoint fj = hitGo.GetComponent<FixedJoint>();

        if (fj){

            Destroy(fj);

        }

    }
 protected override void TryToExplode(GameObject hitGo) {

        if (hitGo.GetComponent<IExplodable>() != null) {

            hitGo.GetComponent<IExplodable>().Activate(heroType, ownerGo);

        }

    }

    public void HideWeapon(){

    }

    public void UpdateLabel(){

    }

    protected override void PlaySoundImpact(RaycastHit hit){
        if (hit.collider.gameObject.GetComponent<RigidbodyInfo>()) { 

            RigidbodyInfo rigidbodyInfo = hit.collider.gameObject.GetComponent<RigidbodyInfo>();

            GameObject soundToInst = soundImpactMetal;

            switch (rigidbodyInfo.rigidbodyType) {

                case RigidbodyType.Iron: soundToInst = soundImpactMetal;  break;
                case RigidbodyType.Stone: soundToInst = soundImpactStone; break;
                case RigidbodyType.Flesh: soundToInst = soundImpactFlesh; break;

            }

            Instantiate(soundToInst, hit.collider.transform.position, Quaternion.identity);

        }
    }

    protected override void MakeToChangeAim(RaycastHit hit){
        if (hit.collider.gameObject.GetComponent<SpawnInfo>() && 
            hit.collider.gameObject.GetComponent<SpawnInfo>().heroType == HeroType.Enemy) {

              hit.collider.gameObject.GetComponent<AI>().aim = GetComponentInParent<SpawnInfo>().gameObject;
            hit.collider.gameObject.GetComponent<AI>().state = State.MoveToAim;

        }
    }

    void TrySetBallOwner(GameObject go) {

        if (go.GetComponent<FootballCollision>()) {

            go.GetComponent<FootballCollision>().ballOwner = HeroType.Player;
            go.GetComponent<FootballCollision>().ballOwnerGo = this.gameObject.GetComponentInParent<SpawnInfo>().gameObject;

        }

    }

}
