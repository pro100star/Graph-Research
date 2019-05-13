namespace WF {
    /// <summary>
    /// Структура пара
    /// </summary>
    /// <typeparam name="T">
    /// Первый тип в паре
    /// </typeparam>
    /// <typeparam name="U">
    /// Второй тип в паре
    /// </typeparam>
    public struct Pair<T, U> {
        /// <summary>
        /// Конструктор пары от двух параметров
        /// </summary>
        /// <param name="first">
        /// Первый параметр
        /// </param>
        /// <param name="second">
        /// Второй параметр
        /// </param>
        public Pair(T first, U second) {
            First = first;
            Second = second;
        }

        /// <summary>
        /// Первое поле
        /// </summary>
        public T First;

        /// <summary>
        /// Второе поле
        /// </summary>
        public U Second;

        /// <summary>
        /// Перегруженный метод ToString()
        /// </summary>
        /// <returns>
        /// Строковое представление текущего объекта
        /// </returns>
        public override string ToString() {
            return $"{First} {Second}";
        }
    }
}
