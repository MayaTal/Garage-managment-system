namespace Ex03.GarageLogic
{
    public class VehicleData
    {
        private readonly Owner r_Owner;
        private readonly Vehicle r_Vehicle;

        public VehicleData(Owner i_Owner, Vehicle i_Vehicle)
        {
            r_Owner = i_Owner;
            r_Vehicle = i_Vehicle;
        }

        public Owner Owner
        {
            get
            {
                return r_Owner;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", r_Owner.ToString(), r_Vehicle.ToString());
        }
    }
}
