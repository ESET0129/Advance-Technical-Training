import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import { useLogger } from "../hooks/useLogger";
import apiClient from "../services/apiClient";
import '../styles/Table.css';
import { FaArrowLeft } from 'react-icons/fa';

export default function BillDetailsPage() {
    const { billId } = useParams();
    const { logInfo, logError } = useLogger();
    const [billDetails, setBillDetails] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        logInfo(`Bill Details(ID: ${billId})`);

        const fetchBillDetails = async () => {
            try {
                setLoading(true);
                const response = await apiClient.get(`/bills/${billId}`);
                setBillDetails(response.data);
            } catch (error) {
                logError('Failed to fetch bill details', error, { billId });
            } finally {
                setLoading(false);
            }
        };
        fetchBillDetails();
    }, [billId, logInfo, logError]);

    if (loading) {
        return <div>Loading bill details...</div>;
    }

    if (!billDetails) {
        return <div>Bill not found. (Check if API server is running!)</div>;
    }

    return (
        <div className="page-container">
            <div className="page-header">
                <Link to="/bills" className="back-link"><FaArrowLeft /></Link>
                <h1>Bill Details – {billDetails.month}</h1>
            </div>

            <div className="table-wrapper">
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>Month</th>
                            <th>Total Amount</th>
                            <th>Due Date</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{billDetails.month}</td>
                            <td>₹{billDetails.amount.toFixed(2)}</td>
                            <td>{billDetails.dueDate}</td>
                            <td>{billDetails.status}</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div className="table-wrapper">
                <table className="data-table">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Reading</th>
                            <th>Consumption</th>
                            <th>Cost</th>
                        </tr>
                    </thead>
                    <tbody>
                        {billDetails.details?.map((item) => (
                            <tr key={item.date}>
                                <td>{item.date}</td>
                                <td>{item.reading}</td>
                                <td>{item.consumption}</td>
                                <td>₹{item.cost.toFixed(2)}</td>
                            </tr>
                        ))}
                        <tr><td>&nbsp;</td><td></td><td></td><td></td></tr>
                        <tr><td>&nbsp;</td><td></td><td></td><td></td></tr>
                    </tbody>
                </table>
            </div>

            <div className="action-buttons">
                <button className="btn btn-secondary">Download PDF</button>
                <button className="btn btn-secondary">Print Bill</button>
                {billDetails.status === 'Pending' && (
                    <button className="btn btn-primary">Pay Now</button>
                )}
            </div>
        </div>
    );
}