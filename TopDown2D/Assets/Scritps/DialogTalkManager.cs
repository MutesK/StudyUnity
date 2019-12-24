using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *   Non-NPC Object ID
 *   100, 101, 102
 *   
 *   NPC Object ID
 *   1000, 1001
 */
public class DialogTalkManager : MonoBehaviour
{
    Dictionary<int, string[]> _DialogMap;
    Dictionary<int, int[]> _DialogPortraitIndexMap;

    void Awake()
    {
        _DialogMap = new Dictionary<int, string[]>();
        _DialogPortraitIndexMap = new Dictionary<int, int[]>();

        GenerateDialog();
    }

    // 원래라면, 파일 Load하여, ID별 대화 리스트들을 초기화 하는게 맞음.
    void GenerateDialog()
    {
        _DialogMap.Add(100, new string[] { "그냥 버려져있는 상자일 뿐이다." });
        _DialogMap.Add(101, new string[] { "무언가 써져있다, 하지만 알아볼수 없다." });
        _DialogMap.Add(102, new string[] { "엄청나게 큰 돌이다." });

        _DialogMap.Add(1000, new string[] { "안녕?", "너 여기 처음 왔구나?" });
        _DialogPortraitIndexMap.Add(1000, new int[] {0,1});
        _DialogMap.Add(1001, new string[] { "뭐야?" , "....?", "귀찮게 하지말고, 내 앞에서 사라져!"});
        _DialogPortraitIndexMap.Add(1001, new int[] {0,2,3});
    }

    public string GetTalk(int ID, int talkIndex)
    {
        int index = talkIndex % _DialogMap[ID].Length;

        return _DialogMap[ID][index];
    }
    public int GetProtraitIndex(int ID, int talkIndex)
    {
        int index = talkIndex % _DialogPortraitIndexMap[ID].Length;

        return _DialogPortraitIndexMap[ID][index];
    }
}
