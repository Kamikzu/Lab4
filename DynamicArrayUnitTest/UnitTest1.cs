using DynamicArrayClass;

namespace DynamicArrayUnitTest
    {
        public class TestDisposable : IDisposable
        {
            public bool _disposed = false;

            public void Dispose()
            {
                _disposed = true;
            }
        }

        [TestClass]
        public class DynamicArrayTests
        {
            private DynamicArray<int> obj;

            [TestInitialize]
            public void Initialize()
            {
                obj = new();
            }

            [DataTestMethod]
            [DataRow(1)]
            [DataRow(5)]
            [DataRow(10)]
            public void Constructor_SetValidValues_CapacityAndLength_Tests(int capacityTarget)
            {
                const int lengthTarget = 0;

                obj = new(capacityTarget);

                Assert.AreEqual(capacityTarget, obj.Capacity);
                Assert.AreEqual(lengthTarget, obj.Length);
            }

            [DataTestMethod]
            [DataRow(0)]
            [DataRow(-5)]
            [DataRow(-50)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_SetInvalidValues_ThrowsException_Tests(int target)
            {
                obj = new(target);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 }, 1)]
            [DataRow(new int[] { 1, 2, 3, 4, 5 }, 5)]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10)]
            public void Constructor_SetArray_Tests(int[] array, int capacity)
            {
                obj = new(array);

                CollectionAssert.AreEqual(array, obj);
                Assert.AreEqual(capacity, obj.Capacity);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
            public void Constructor_SetObject_Tests(int[] array)
            {
                DynamicArray<int> temp = new(array);

                obj = new(temp);

                CollectionAssert.AreEqual(temp, obj);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 }, 8)]
            [DataRow(new int[] { 1, 2, 3, 4, 5 }, 8)]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 16)]
            public void Method_Add_CapacityAndLength_Tests(int[] array, int capacity)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    obj.Add(array[i]);
                }

                CollectionAssert.AreEqual(array, obj);
                Assert.AreEqual(array.Length, obj.Length);
                Assert.AreEqual(capacity, obj.Capacity);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
            public void Method_AddRange_SetArray_Tests(int[] array)
            {
                DynamicArray<int> temp = new(array);

                obj.AddRange(array);

                CollectionAssert.AreEqual(temp, obj);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
            public void Method_AddRange_SetObject_Tests(int[] array)
            {
                DynamicArray<int> temp = new(array);

                obj.AddRange(temp);

                CollectionAssert.AreEqual(temp, obj);
            }

            [TestMethod]
            public void Method_Remove_4_Number_Test()
            {
                int[] target = { 1, 3, 5, 7, 8 };
                int[] original = { 1, 3, 4, 5, 7, 4, 8, 4 };

                obj = new(original);
                obj.Remove(4);

                CollectionAssert.AreEqual(target, obj);
            }

            [TestMethod]
            public void Method_Remove_5_NumberNearby_Test()
            {
                int[] target = { 1, 3, 3, 7, 9 };
                int[] original = { 1, 3, 5, 5, 3, 5, 7, 5, 5, 5, 9 };

                obj = new(original);
                obj.Remove(5);

                CollectionAssert.AreEqual(target, obj);
            }

            [TestMethod]
            public void Method_Insert_To_0_Index_Test()
            {
                int[] target = { 5, 1, 3, 5, 7, 8 };
                int[] original = { 1, 3, 5, 7, 8 };

                obj = new(original);
                obj.Insert(5, 0);

                CollectionAssert.AreEqual(target, obj);
            }

            [TestMethod]
            public void Method_Insert_To_3_Index_Test()
            {
                int[] target = { 1, 3, 5, 5, 7, 8 };
                int[] original = { 1, 3, 5, 7, 8 };

                obj = new(original);
                obj.Insert(5, 3);

                CollectionAssert.AreEqual(target, obj);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Method_Insert_To_InvalidIndex_ThrowsException_Test()
            {
                int[] original = { 1, 3, 5, 7, 8 };

                obj = new(original);
                obj.Insert(5, 10);
            }

            [TestMethod]
            public void Ienumerable_ForEach_Object_Test()
            {
                int[] target = { 1, 2, 3, 4, 5, 6 };
                int[] result = new int[6];
                int i = 0;

                obj = new(target);

                foreach (var item in obj)
                {
                    result[i] = item;
                    i++;
                }

                CollectionAssert.AreEqual(target, result);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 }, -1)]
            [DataRow(new int[] { 1, 2, 3, 4, 5 }, 5)]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 100)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Indexator_SetInvalidValues_ThrowsException_Tests(int[] array, int index)
            {
                obj = new(array);

                obj[index] = 0;
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 }, -1)]
            [DataRow(new int[] { 1, 2, 3, 4, 5 }, 5)]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 100)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Indexator_GetInvalidValues_ThrowsException_Tests(int[] array, int index)
            {
                obj = new(array);

                int s = obj[index];
            }

            [TestMethod]
            public void Indexator_SetValidValue_Test()
            {
                int[] target = { 1, 2, 5, 4, 5 };
                int[] original = { 1, 2, 3, 4, 5 };

                obj = new(original);
                obj[2] = 5;

                Assert.AreEqual(target, obj);
            }

            [TestMethod]
            public void Indexator_GetValidValue_Test()
            {
                int[] original = { 1, 2, 3, 4, 5 };

                obj = new(original);

                Assert.AreEqual(obj[2], 3);
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
            public void Method_Equals_ValidObjects_Tests(int[] array)
            {
                obj = new(array);
                DynamicArray<int> temp = new(array);

                Assert.IsTrue(obj.Equals(temp));
            }

            [DataTestMethod]
            [DataRow(new int[] { 1 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5 })]
            [DataRow(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })]
            public void Method_Equals_InvalidObjects_Tests(int[] array)
            {
                obj = new(array);
                DynamicArray<int> temp = new(array);
                temp.Add(1);

                Assert.IsFalse(obj.Equals(temp));
            }

            [TestMethod]
            public void Method_DisposeObject_Test()
            {
                var array = new DynamicArray<TestDisposable>();
                var check = new TestDisposable();

                array.Add(new TestDisposable());
                array.Add(new TestDisposable());
                array.Add(new TestDisposable());

                array.Add(check);
                array.Dispose();

                Assert.IsTrue(check._disposed);
                Assert.AreEqual(0, array.Capacity);
                Assert.AreEqual(0, array.Length);
                Assert.IsTrue(array._disposed);
            }

            [TestMethod]
            public void EventTriggered_OnCapacityChange_Test()
            {
                obj = new(1);
                bool eventTriggered = false;

                obj.Notify += (sender, args) =>
                {
                    eventTriggered = true;
                    Assert.AreEqual(1, args.OldCapacity);
                    Assert.AreEqual(2, args.NewCapacity);
                };

                obj.Add(1);
                obj.Add(2);

                Assert.IsTrue(eventTriggered);
            }
        }
    }