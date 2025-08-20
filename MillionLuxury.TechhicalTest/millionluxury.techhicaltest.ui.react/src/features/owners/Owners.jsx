import { useState, useEffect } from "react";
import Card from "../../components/molecules/Card";
import { fetchOwners } from "./ownersRepository.js";

export default function Owners() {
	const [isFetching, setIsFetching] = useState(false);
	const [responseOwners, setResponse] = useState([]);
	const [error, setError] = useState(null);

	useEffect(() => {
		async function fetchData() {
			try {
				setIsFetching(true);
				const owners = await fetchOwners(`$filter=contains(Name, 'nombre')&$orderby=name desc`);
				setResponse(owners);
			} catch (err) {
				setError(err.message);
			} finally {
				setIsFetching(false);
			}
		}

		fetchData();
	}, []);

	return (
		<Card title="Owners">
			<table className="table">
				<thead>
					<tr>
						<th scope="col">Actions</th>
						<th scope="col">Name</th>
						<th scope="col">Addres</th>						
					</tr>
				</thead>
				<tbody>
					{/*{isFetching && (<p>Cargando</p>) }*/}
					{responseOwners.Data && responseOwners.Data.map((owner) => (
						<tr key={owner.Id}>
							<td>Actions</td>
							<td>{owner.Name}</td>
							<td>{owner.Address}</td>
						</tr>
					))}
				</tbody>
			</table>
		</Card>
	);
}