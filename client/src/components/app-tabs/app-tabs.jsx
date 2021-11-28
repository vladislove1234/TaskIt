import React from 'react';
import {useSelector} from 'react-redux';

import CreateTeam from '../create-team/create-team';
import TeamChat from '../team-chat/team-chat';
import TeamMembers from '../team-members/team-members';
import TeamTasks from '../team-tasks/team-tasks';

import './app-tabs.scss';

const AppTabs = () => {
  const appWindow = useSelector(({app}) => app.window);

  const windows = {
    'createTeam': <CreateTeam />,
    'team_tasks': <TeamTasks />,
    'team_members': <TeamMembers />,
    'team_chat': <TeamChat />,
  };

  return windows[appWindow] || <p>it shines and fades</p>;
};

export default AppTabs;
