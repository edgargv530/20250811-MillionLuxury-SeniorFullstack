export async function fetchOwners(queryString) {
	const url = `https://localhost:7178/api/owners${queryString ? `${"?"}${queryString}` : ''}`;

	//console.log(`Fetching owners from: ${url}`);
	const response = await fetch(url, {
		method: 'GET',
		headers: {
			'Access-Control-Allow-Headers': 'application/json; charset=utf-8'
		}
	});

	const resData = await response.json();
	if (!response.ok) {
		throw new Error('Could not fetch owners.');
	}

	return resData;
}