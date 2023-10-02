using SimpleCode.SimpleAnonumousThype;
using SimpleCode.SimpleDelegates;
using SimpleCode.SimpleEnumeration;
using SimpleCode.SimpleGeneralizationType;
using SimpleCode.SimpleMyNumerator;
using SimpleCode.SimpleObjectType;
using SimpleCode.SimplePointers;
using SimpleCode.SimpleReloadOperations;

using UnityEngine;

namespace SimpleCode {
    public class Main : MonoBehaviour {

        private void Start() {

            new ObjectType();// примеры распоаковки и упаковки данных в обьект и наоборот
            new MyNumerator();// пример создания своего счетчика двумя способами
            new GeneralizationType();// пример работы обобщенного типа
            new Enumeration();// пример работы с enum
            new Delegates();// пример работы делегатов
            new ReloadOperations();// 
            new AnonimousThype();// 
            new Pointers();//

            /*
            new SimpleEncoding();// ������ ������ � �����������
            new SimpleDateTime();// ������ ������ � ����� � ��������
            new SimpleToStringAndParse();// ������ ������ ToString � Parse
            new SimpleFormat();// ������ �������������� �����
            new SimpleConvert();// ������ ������ � ������� Convert
            new SimpleBigInteger();// ������ ��������� BigInteger
            new SimpleComplex();// ������ ��������� Complex
            new SimpleRandom();// ������ ������ � Random
            new SimpleTuple();// ������ ������ ��������
            new SimpleGuid();// ������ ������ ��������� Guid
            new SimpleClassProcess();// ������ ������ ������ Process
            */
            }

        }
    }