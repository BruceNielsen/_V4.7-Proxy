/*
 * Copyright (C) Henrik Jonsson 2007
 *
 * THIS WORK IS PROVIDED UNDER THE TERMS OF THIS CODE PROJECT OPEN LICENSE ("LICENSE").
 * THE WORK IS PROTECTED BY COPYRIGHT AND/OR OTHER APPLICABLE LAW. ANY USE OF THE WORK
 * OTHER THAN AS AUTHORIZED UNDER THIS LICENSE OR COPYRIGHT LAW IS PROHIBITED.
 *
 * See licence.txt or http://thecodeproject.com/info/eula.aspx for full licence description.
 *
 * This copyright notice must not be removed.
*/

namespace Reversible
{
    /// <summary>
    /// Abstract base class for all reversible changes.
    /// </summary>
    public abstract class Edit
    {
        /// <summary>
        /// Utility function to switch between two values handy when reversing some operations.
        /// </summary>
        /// <typeparam name="T">Type of variable</typeparam>
        /// <param name="var1">Reference to first variable</param>
        /// <param name="var2">Reference to second variable</param>
        protected static void Switch<T>(ref T var1, ref T var2)
        {
            T oldvar1 = var1;
            var1 = var2;
            var2 = oldvar1;
        }

        /// <summary>
        /// Reverses the operation (undoing or redoing depending on state in transaction)
        /// </summary>
        /// <returns>An Edit instance representing the operation that reverses the effect of the done reversal (usual the same instance).</returns>
        public abstract Edit Reverse();

        /// <summary>
        /// Returns the name of this reversible operation (or null if no name is defined)
        /// </summary>
        public virtual string Name
        {
            get { return null; }
        }
    }
}