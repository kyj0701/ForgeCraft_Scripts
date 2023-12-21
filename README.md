# ForgeCraft

## 📌 주요 기능

### 대장간
![Casting](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/919fcc99-691f-49a7-a602-998300974455)
![Forging](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/35077f5a-1447-4899-987e-5b518eb7105f)


### 전투
![Battle](https://github.com/UnityFourge/ForgeCraft_Scripts/assets/31722243/a8c30cb9-64e1-417f-93a2-54d79bf173f2)


## 📌 주요 기술

### Singleton으로 공유되는 필드와 메서드 관리
* 여러가지 필드와 자료들을 중복해서 생성하지 않고 여러 스크립트에서 공유하기위해 정적 객체를 활용.
* 씬이 전환될 때에도 유지되어야 할 정보를 가진 정적 객체는 DontDestroyOnLoad를 활용하여 객체가 파괴되는 것을 방지.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/Singleton.cs)

### 캐릭터와 적의 동작을 FSM으로 구현

* 캐릭터와 적의 동작을 FSM을 활용하여 구현.
* 단순히 bool 변수로 동작의 변화를 확인하기보다는 상태가 변화하는 것에 따라 동작을 만들어주는 것이 다양한 상태와 동작에 대응하기 효율적이라 판단.
* 상태가 변화함에 따라 애니메이터의 트리거를 변화시켜 동작에 맞는 애니메이션을 재생.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/FSM.cs)

### Unity 프로파일러를 통한 성능 저하 요소 탐색

* 프로파일러를 새 기능이 만들어질 때마다 확인하여 성능 저하 여부 확인.
* 성능이 저하되었다면 원인을 분석하여 해결. (인스턴스의 반복 생성, Find와 GetComponent의 반복 사용 등)

### 중복되는 데이터를 ScriptableObject로 관리
* 캐릭터, 적, 아이템 등 다양한 요소의 정보를 관리하기 위해 활용.
* 기본이 되는 정보들을 중복해서 생성하지 않기 위해 ScriptableObject로 관리.
* 중복해서 생성하지 않기 때문에 메모리 상의 이점.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/ScriptableObject.cs)

### 다수의 효과음을 관리하기 위해 Object Pool 활용

* 다수의 효과음이 동시 재생되어야 하는 상황.
* AudioSource를 가진 오브젝트를 프리팹으로 만들고 Object Pool을 통해 관리.
* 오브젝트가 반복적으로 생성, 파괴되는 것을 방지.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/ObjectPool.cs)

### Coroutine 최적화를 위해 Coroutine Helper 구현

* 코루틴 활용 시, 서브 루틴의 지연을 주기 위해 new WaitForSeconds()를 통해 WaitForSeconds를 생성.
* 생성된 WaitForSeconds는 결국 가비지를 발생시키기 때문에 서브 루틴이 실행될 때마다 가비지가 발생.
* 가지비 컬렉터가 호출되어 성능 저하를 유발.
* 시간(float)을 key, WaitForSeconds를 value로 갖는 딕셔너리에 저장하고 불러오면서 사용함으로써 가비지의 발생을 줄여 성능 저하를 방지.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/CoroutineHelper.cs)

### StringBuilder로 문자열 인스턴스 반복 생성 방지

* 문자열끼리 더하는 연산은 인스턴스를 생성, 가비지를 발생시켜 GC를 호출하며 성능을 저하.
* StringBuilder의 Append 메서드를 활용하여 더하기 연산을 수행해서 인스턴스가 반복적으로 생성되는 것을 방지.
* [LINK](https://github.com/kyj0701/ForgeCraft_Scripts/blob/main/Scripts/StringBuilder.cs)


## 📌 링크 

### 팀 노션 페이지
https://teamsparta.notion.site/Fourge-da16c06db3ba48e5bd5d2065af1730d1

### 팀 피그마 페이지
https://www.figma.com/file/o8HRYDYF4F5t6k1sW918dE/ForgeCraft?type=whiteboard&node-id=0-1&t=ODYpyiAG9hgrzYck-0
