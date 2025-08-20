export default function UserInfo({ title, userName, userEmail }) {
	return (
		<div className="card">
			<div className="card-header">
				{title}
			</div>
			<div className="card-body">
				<h5 className="card-title">{userName}</h5>
				<p className="card-text">{userEmail}</p>
			</div>
		</div >
	);
}