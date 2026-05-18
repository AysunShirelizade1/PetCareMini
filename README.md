# AzTU-PeerZenith

# Branching strategy

![Logo](https://d33wubrfki0l68.cloudfront.net/42fea6b4216e2060ea860e0b86f03d770ef9cbd3/3b149/uploads/git-flow-diagram.png)


___

# Code and issue managment lifecycle
- **opened** : Issue ilk dəfə yaradılanda bu siyahıya düşür. Issue yaradılarkən bu label-lar istifadə edilməlidir: *feature*, *hotfix*.
- **ToDo** : Qərar verildikdə ki, *opened* siyahısındakı hansısa issue artıq həll edilməlidir, bu halda həmin issue *opened* siyahısından *todo* siyahısına atılır.
- **InProgress** : Developer *ToDo*da yerləşən issue üzərində lokalında işə başladıqda həmin issue-nu *InProgress* siyahısına atır.
- Developer lokalında işi bitirdikdən sonra və ya həmin taskın hazır olduğuna əmin olduqdan sonra öz branch-ını *develop* branch-ına merge etmək üçün request yaradır.
- **Solved** : Developer, branch-ının merge request-i qəbul edildikdən sonra həmin issue-nu *InProgress*dən *Solved* siyahısına atır.
- **Release(Test)** : Developer,artıq bu issue-nun testə hazır olduğuna əmin olursa bu halda issue bu siyahıya əlavə olunur və test ünvanı update olunarkən yalnız bu siyahıda olan tasklar keçir.
	*Develop* branch-ından *release* branch-ı yaradan developer, *release* branch-ını yaratdığı commitə tag əlavə edir(r-v1.1). Həmçinin *release*ə keçən issue-ları *solved* siyahısından *release* siyahısına atır.
  - Test edən şəxslər, *test* ünvanı update olunduqdan sonra *release* siyahısında olan məsələləri bir-bir test etməli:
  - Testdən uğurla keçən issue-a *Release(success)* label-ı əlavə edir
  - Testdən uğurla keçmədiyi halda isə *Release(failed)* label-ı əlavə edir.
  - Taskına *Release(failed)* label-ı əlavə edilmiş developer cari release branch-ını lokalına pull etdikdən sonra onun üzərində işləyərək səhvləri həll etdikdən sonra həmin branch-ı push edir. Həmçinin həmin issue-dan *Release(failed)* label-ını silir ki, tester yenidən həmin taskı test etsin.
- *Release* siyahısındakı bütün issue-lara *Release(success)* label-ı əlavə edildikdən sonra developer *release* branch-ını *master* branch-ına merge edir. Həmçinin *master*də həmin merge commitinə yeni tag əlavə edir(v1.1). Həmçinin, *Release* siyahısındakı bütün issue-ları *Production* siyahısına atır.
- **Release(Failed)** : Test ünvanında testdən keçməyən task bu siyahıya əlavə olunur.
- **Release(success)** : Test ünvanında testdən keçən tasklar bu siyahıya əlavə olunur. 
- **Production** : Testerlər *Production* siyahısında olan işləri bir-bir test edir:
  - Hər issue-nun testi bitdikdən sonra, testdən uğurla keçdiyi halda ona *Production(Success)*, keçmədiyi halda isə *Production(Failed)* label-ə əlavə edilir.
  - Production-da testi bitdikdən sonra bütün issue-lar *closed* siyahısına atılır. Bundan sonra testdən keçməyən tasklar üçün yeni hotfix issue yazılır.

## Hotfix issue
- Hotfix issue-lar developer tərəfindən həll edildikdən sonra və hotfix branch-ı *master* və *develop* branch-ına merge edilib, sayt deploy edildikdən sonra həmin issue solved siyahısına atılmır.
  - Bunun yerinə issue-a *release* və *production* label-larını əlavə edir. Bu halda issue həm *release*, həm də *production* siyahısında görünəcək.
- Bundan sonra test edilir:
  - Testdən uğurla keçən taska *Production(Success)* label-ı əlavə edilir.
  - Testdən uğurla keçmədiyi halda isə *Production(Failed)* label-ı əlavə edilir.


### Qeyd
Fors major hallar gitlab maintainer-lərlə məsləhətləşib qərar verilməlidir.

___

# Branch naming convention

Short-term branch-lar 2 növdür: feature, hotfix.

#### 1. Feature branch aşağıdakı kimi adlandırılmalı:

```bash
  feature/<tasknomresi>/<taskin-qisa-izahi-və-ya-taskin-adi>
```

#### 2. Hotfix branch aşağıdakı kimi adlandırılmalı:

```bash
  hotfix/<tasknomresi>/<taskin-qisa-izahi-və-ya-taskin-adi>
```

# Release branch naming convention

```bash
  release/<versiya nömrəsi>

  Nümunə:
  release/1.0
  release/2.3
```


# Tag naming convention

```bash
  r-v<versiya nömrəsi>      => develop branch
  v<versiya nömrəsi>        => master branch
  
  Nümunə:
  r-v1.5.2
  v1.5.2
```


# Commit message convention

Commit edilən fayllarda görülmüş işlərin geniş izahı verilməlidir


# Qeydlər

```bash
  <packages> adlı folder yaratmayın. Yaradıldığı halda .gitignore onun stage olunmasının qarşısını alacaq
```
