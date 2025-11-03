import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

const data = [
    { month: "June", sales: 6000 },
    { month: "July", sales: 5600 },
    { month: "Aug", sales: 4000 },
    { month: "Sep", sales: 6000 },
    { month: "Oct", sales: 3400 },
];

function LineCharts({ width = 800, height = 300 }) {
    return (
        
        <>
            <LineChart width={width} height={height} data={data}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="month" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Line 
                    type="monotone" 
                    dataKey="sales" 
                    stroke="#c62809ff" 
                    activeDot={{ r: 8 }} 
                />
            </LineChart>
        </>
    );
}

export default LineCharts;